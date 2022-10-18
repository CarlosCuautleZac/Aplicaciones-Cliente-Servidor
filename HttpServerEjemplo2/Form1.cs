namespace HttpServerEjemplo2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        ServidorHttp servidor = new();

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            servidor.Iniciar();
            btnDetener.Enabled = true;
            btnIniciar.Enabled = false;
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            servidor.Detener();
            btnDetener.Enabled = false;
            btnIniciar.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            servidor.MensajeRecibido += Servidor_MensajeRecibido;
        }

        private void Servidor_MensajeRecibido(string titulo, string mensaje)
        {
            notifyIcon1.ShowBalloonTip(3000, titulo, mensaje, ToolTipIcon.Info);
        }
    }
}