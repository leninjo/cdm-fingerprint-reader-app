using libzkfpcsharp;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace LectorHuella
{
    public partial class Form1 : Form
    {
        private bool isReaderInitialized = false;
        private bool isCheckingReaderStatus = false;

        private readonly zkfp fpInstance = new();

        private readonly Color errorColor = Color.FromArgb(0, 169, 50, 38);
        private readonly Color successColor = Color.FromArgb(0, 17, 122, 101);
        private readonly Color loadingColor = Color.FromArgb(0, 36, 113, 163);

        private String UUID = "";

        private readonly Dictionary<int, string> dictionary = new()
        {
            {  1,  "La SDK ya está inicializado" },
            { -1,  "Error al inicializar SDK" },
            { -2,  "Error al inicializar el lector" },
            { -3,  "No se encontró ningún dispositivo conectado" },
            { -4,  "Operación no soportada" },
            { -5,  "Parámetro inválido en la función" },
            { -6,  "Error al abrir el dispositivo" },
            { -7,  "Manejador del dispositivo inválido" },
            { -8,  "Error al capturar la huella" },
            { -9,  "Error al extraer la huella dactilar" },
            { -10, "Proceso abortado" },
            { -11, "Memoria insuficiente" },
            { -12, "El dispositivo está ocupado" },
            { -13, "Error al agregar la huella a la base de datos" },
            { -14, "Error al eliminar la huella de la base de datos" },
            { -17, "Fallo en la operación" },
            { -18, "Operación cancelada" },
            { -20, "Error al verificar la huella dactilar" },
            { -22, "Error al combinar varias huellas" },
            { -23, "El dispositivo no está abierto" },
            { -24, "La biblioteca no está inicializada" },
            { -25, "El dispositivo ya está abierto" },
            { -26, "Error al cargar la imagen de la huella" },
            { -27, "Error al analizar la imagen de la huella" },
            { -28, "Tiempo de espera agotado" }
        };

        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckReaderStatus();
        }

        private async void CheckReaderStatus()
        {
            if (isCheckingReaderStatus) return;

            isCheckingReaderStatus = true;
            ReaderConnect.Enabled = false;

            StatusLabel.ForeColor = loadingColor;
            StatusLabel.Text = "Verificando lector...";
            StatusLabel.Refresh();
            await Task.Delay(100);

            await Task.Run(() =>
            {
                if (!isReaderInitialized)
                {
                    int result = fpInstance.Initialize();

                    if (result == zkfperrdef.ZKFP_ERR_OK)
                    {
                        isReaderInitialized = true;
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            StatusLabel.ForeColor = errorColor;
                            StatusLabel.Text = "No se pudo iniciar el lector, verifique conexión.";
                        }));
                    }
                }

                fpInstance.CloseDevice();
                int deviceCount = fpInstance.GetDeviceCount();

                this.Invoke(new Action(() =>
                {
                    if (deviceCount > 0)
                    {
                        int result = fpInstance.OpenDevice(0);
                        if (result == zkfperrdef.ZKFP_ERR_OK)
                        {
                            int width = fpInstance.imageWidth;
                            int height = fpInstance.imageHeight;

                            StatusLabel.ForeColor = successColor;
                            StatusLabel.Text = $"Lector conectado. Resolución: {width}x{height}";
                        }
                        else
                        {
                            StatusLabel.ForeColor = errorColor;
                            StatusLabel.Text = "No fue posible acceder al lector";
                        }
                    }
                    else
                    {
                        StatusLabel.ForeColor = errorColor;
                        StatusLabel.Text = "No se encontró ningún lector conectado.";
                        fpInstance.Finalize();
                        isReaderInitialized = false;
                    }
                }));
            });

            isCheckingReaderStatus = false;
            ReaderConnect.Enabled = true;
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            CheckReaderStatus();
        }

        private async void ReadFingeprint_Click(object sender, EventArgs e)
        {
            ReadFingeprint.Enabled = false;

            StatusLabel.ForeColor = loadingColor;
            StatusLabel.Text = "Leyendo huella...";

            int size = 2048;
            byte[] imgBuffer = new byte[fpInstance.imageWidth * fpInstance.imageHeight];
            byte[] templateBuffer = new byte[size];

            int result = fpInstance.AcquireFingerprint(imgBuffer, templateBuffer, ref size);

            if (result == zkfperrdef.ZKFP_ERR_OK)
            {
                await ConvertToBitmapAndSendToApi(imgBuffer, fpInstance.imageWidth, fpInstance.imageHeight);
            }
            else
            {
                StatusLabel.ForeColor = errorColor;

                if (dictionary.TryGetValue(result, out string? errorMessage))
                {
                    StatusLabel.Text = errorMessage;
                }
                else
                {
                    StatusLabel.Text = "No fue posible leer la huella";
                }

            }

            ReadFingeprint.Enabled = true;
        }

        private static readonly HttpClient client = new() { Timeout = TimeSpan.FromSeconds(10) };

        private async Task ConvertToBitmapAndSendToApi(byte[] imgBuffer, int width, int height)
        {
            Bitmap bmp = new(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            System.Drawing.Imaging.ColorPalette palette = bmp.Palette;
            for (int i = 0; i < 256; i++)
                palette.Entries[i] = Color.FromArgb(i, i, i);
            bmp.Palette = palette;

            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(
                new Rectangle(0, 0, width, height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            Marshal.Copy(imgBuffer, 0, bmpData.Scan0, imgBuffer.Length);
            bmp.UnlockBits(bmpData);

            try
            {
                string tempFilePath = Path.Combine(Path.GetTempPath(), "huella.bmp");
                bmp.Save(tempFilePath, System.Drawing.Imaging.ImageFormat.Bmp);

                if (!File.Exists(tempFilePath))
                {
                    throw new FileNotFoundException("El archivo temporal no se creó correctamente");
                }

                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8080/fingerprint");
                var content = new MultipartFormDataContent();

                using var fileStream = File.OpenRead(tempFilePath);
                var fileContent = new StreamContent(fileStream);
                content.Add(fileContent, "file", "huella.bmp");
                request.Content = content;

                try
                {
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var fingerprintResponse = JsonSerializer.Deserialize<FingerprintResponse>(responseContent);

                    this.Invoke(new Action(() =>
                    {
                        if (fingerprintResponse != null)
                        {
                            UUIDLabel.Text = fingerprintResponse.uuid;
                            UUID = fingerprintResponse.uuid;

                            StatusLabel.ForeColor = successColor;
                            StatusLabel.Text = "Huella leída exitosamente";

                            ShowFingerprint.SizeMode = PictureBoxSizeMode.Zoom;
                            ShowFingerprint.Image = bmp;
                        }
                        else
                        {
                            StatusLabel.ForeColor = errorColor;
                            StatusLabel.Text = "Respuesta del servidor inválida";
                        }
                    }));
                }
                catch (HttpRequestException)
                {
                    this.Invoke(new Action(() =>
                    {
                        StatusLabel.ForeColor = errorColor;
                        StatusLabel.Text = "No fue posible enviar la huella al servidor";
                    }));
                }
            }
            catch (ExternalException)
            {
                this.Invoke(new Action(() =>
                {
                    StatusLabel.ForeColor = errorColor;
                    StatusLabel.Text = "No fue posible almacenar la huella";
                }));
            }
        }

        private void CopyUUID_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(UUID);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}

public class FingerprintResponse
{
    public required string uuid { get; set; }
}
