using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Network_Programming_Task5;

public partial class MainWindow : Window, INotifyPropertyChanged
{
    private ObservableCollection<MimeMessage> mails = new();


    public ObservableCollection<MimeMessage> Mails { get => mails; set { mails = value; OnPropertyChanged(); } }

    private async Task GetMailsAsync(string _folderName = "Inbox")
    {
        var obj=new object();
        try
        {
            using var imap = new ImapClient();
            await imap.ConnectAsync("imap.gmail.com", 993, true);
            await imap.AuthenticateAsync("qasimov.vaqif512@gmail.com", "aphh stkt mdoe yzxa");

            var folder = imap.GetFolder(_folderName);
            await folder.OpenAsync(FolderAccess.ReadOnly);

            var ids = await folder.SearchAsync(SearchQuery.All);
            var tempMails = new ObservableCollection<MimeMessage>();
            Mails = [];
            foreach (var id in ids)
            {
                var message = await folder.GetMessageAsync(id);
                Application.Current.Dispatcher.Invoke(new(() =>
                {
                    Mails.Add(message);
                }));
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error retrieving emails: {ex.Message}");
        }
    }


    public MainWindow()
    {
        InitializeComponent();
        var a = new MimeMessage();
        DataContext = this;
        _ = GetMailsAsync();
    }

    private void InboxBtn_Click(object sender, RoutedEventArgs e)
    {
        _ = GetMailsAsync();
    }

    private void StarredBtn_Click(object sender, RoutedEventArgs e)
    {
        _ = GetMailsAsync("[Gmail]/Starred");
    }

    private void SnooozdBtn_Click(object sender, RoutedEventArgs e)
    {

    }

    private void SentBtn_Click(object sender, RoutedEventArgs e)
    {
        _ = GetMailsAsync("[Gmail]/Sent Mail");
    }

    private void DraftBtn_Click(object sender, RoutedEventArgs e)
    {

    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {

    }


    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}