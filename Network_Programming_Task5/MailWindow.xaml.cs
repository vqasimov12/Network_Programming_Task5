using MimeKit;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Network_Programming_Task5;

public partial class MailWindow : Window, INotifyPropertyChanged
{
    private MimeMessage mail;
    private string mailBody;

    public MimeMessage Mail { get => mail; set { mail = value; OnPropertyChanged(); OnPropertyChanged(nameof(MailBody)); } }
    public MailWindow()
    {
        InitializeComponent();
        DataContext = this;
    }
    public string MailBody => DecodeBody(Mail);

    private string DecodeBody(MimeMessage mimeMessage)
    {
        if (mimeMessage.Body is Multipart multipart)
        {
            var textPart = multipart.OfType<TextPart>().FirstOrDefault();
            if (textPart != null)
                return textPart.Text;
        }
        return string.Empty;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
