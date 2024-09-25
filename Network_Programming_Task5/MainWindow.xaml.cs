using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Network_Programming_Task5;

public partial class MainWindow : Window, INotifyPropertyChanged
{
    private ObservableCollection<MimeMessage> mails = new();


    public ObservableCollection<MimeMessage> Mails { get => mails; set { mails = value; OnPropertyChanged(); } }

    private async Task GetMailsAsync(SpecialFolder a=SpecialFolder.All)
    {
        var obj=new object();
        try
        {
            using var imap = new ImapClient();
            await imap.ConnectAsync("imap.gmail.com", 993, true);
            await imap.AuthenticateAsync("qasimov.vaqif512@gmail.com", "aphh stkt mdoe yzxa");

            var folder = imap.GetFolder(a);
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
            MessageBox.Show(ex.Message);
        }
    }


    public MainWindow()
    {
        InitializeComponent();
        var a = new MimeMessage();
        DataContext = this;
        //_ = GetMailsAsync();
    }

    private void InboxBtn_Click(object sender, RoutedEventArgs e)
    {
        _ = GetMailsAsync();
    }

    private void StarredBtn_Click(object sender, RoutedEventArgs e)
    {
        _ = GetMailsAsync(SpecialFolder.Important);
    }

    private void SnooozdBtn_Click(object sender, RoutedEventArgs e)
    {

    }

    private async void SentBtn_Click(object sender, RoutedEventArgs e)
    {
        await GetMailsAsync(SpecialFolder.Sent);
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

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        var window = new ComposeMail();
        window.ShowDialog();
    }

    private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var list = sender as ListView;
        if (list is null) return;
        var window = new MailWindow();

        window.Mail = list.SelectedItem as MimeMessage;
        window.ShowDialog();
    }
}