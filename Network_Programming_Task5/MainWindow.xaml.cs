using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Network_Programming_Task5;

public partial class MainWindow : Window, INotifyPropertyChanged
{
    private ObservableCollection<MimeMessage> inboxMails = new();
    private ObservableCollection<MimeMessage> sentMails = new();
    private ObservableCollection<MimeMessage> snoozedMails = new();
    private ObservableCollection<MimeMessage> draftMails = new();
    private ObservableCollection<MimeMessage> starredMails = new();


    public ObservableCollection<MimeMessage> InboxMails { get => inboxMails; set { inboxMails = value; OnPropertyChanged(); } }
    public ObservableCollection<MimeMessage> SentMails { get => sentMails; set { sentMails = value; OnPropertyChanged(); } }
    public ObservableCollection<MimeMessage> SnoozedMails { get => snoozedMails; set { snoozedMails = value; OnPropertyChanged(); } }
    public ObservableCollection<MimeMessage> DraftMails { get => draftMails; set { draftMails = value; OnPropertyChanged(); } }
    public ObservableCollection<MimeMessage> StarredMails { get => starredMails; set { starredMails = value; OnPropertyChanged(); } }

    private async Task GetMailsAsync(SpecialFolder a = SpecialFolder.All)
    {
        var obj = new object();
        //try
        //{
        //    using var imap = new ImapClient();
        //    await imap.ConnectAsync("imap.gmail.com", 993, true);
        //    await imap.AuthenticateAsync("qasimov.vaqif512@gmail.com", "aphh stkt mdoe yzxa");

        //    var folder = imap.GetFolder(a);
        //    await folder.OpenAsync(FolderAccess.ReadOnly);

        //    var ids = await folder.SearchAsync(SearchQuery.All);
        //    var tempMails = new ObservableCollection<MimeMessage>();
        //    Mails = [];
        //    foreach (var id in ids)
        //    {
        //        var message = await folder.GetMessageAsync(id);
        //        Application.Current.Dispatcher.Invoke(new(() =>
        //        {
        //            Mails.Add(message);
        //        }));
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(ex.Message);
        //}
    }


    public MainWindow()
    {
        InitializeComponent();
        var a = new MimeMessage();
        DataContext = this;
        //_ = GetMailsAsync();
        InboxCommand = new RelayCommand(InboxCommandExecute);
        SentCommand = new RelayCommand(SentCommandExecute);
        StarredCommand = new RelayCommand(StarredCommandExecute);
        SnoozedCommand = new RelayCommand(SnoozedCommandExecute);
        DraftCommand = new RelayCommand(DraftCommandExecute);
    }


    public ICommand InboxCommand { get; set; }
    public async void InboxCommandExecute(object? obj)
    {
        if (obj is ListView list)
        {
            try
            {
                using var imap = new ImapClient();
                await imap.ConnectAsync("imap.gmail.com", 993, true);
                await imap.AuthenticateAsync("qasimov.vaqif512@gmail.com", "aphh stkt mdoe yzxa");

                var folder = imap.GetFolder("Inbox");
                await folder.OpenAsync(FolderAccess.ReadOnly);

                var ids = await folder.SearchAsync(SearchQuery.All);
                InboxMails = [];
                list.ItemsSource = InboxMails;
                foreach (var id in ids)
                {
                    var message = await folder.GetMessageAsync(id);
                    Application.Current.Dispatcher.Invoke(new(() =>
                    {
                        InboxMails.Add(message);
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public ICommand SentCommand { get; set; }
    public async void SentCommandExecute(object? obj)
    {
        if (obj is ListView list)
        {
            try
            {
                using var imap = new ImapClient();
                await imap.ConnectAsync("imap.gmail.com", 993, true);
                await imap.AuthenticateAsync("qasimov.vaqif512@gmail.com", "aphh stkt mdoe yzxa");

                var folder = imap.GetFolder(SpecialFolder.Sent);
                await folder?.OpenAsync(FolderAccess.ReadOnly);

                var ids = await folder?.SearchAsync(SearchQuery.All);
                SentMails = [];
                list.ItemsSource = SentMails;
                foreach (var id in ids)
                {
                    var message = await folder.GetMessageAsync(id);
                    Application.Current.Dispatcher.Invoke(new(() =>
                    {
                        SentMails.Add(message);
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public ICommand StarredCommand { get; set; }
    public async void StarredCommandExecute(object? obj)
    {
        if (obj is ListView list)
        {
            try
            {
                using var imap = new ImapClient();
                await imap.ConnectAsync("imap.gmail.com", 993, true);
                await imap.AuthenticateAsync("qasimov.vaqif512@gmail.com", "aphh stkt mdoe yzxa");

                var folder = imap.GetFolder(SpecialFolder.Flagged);
                await folder?.OpenAsync(FolderAccess.ReadOnly);

                var ids = await folder?.SearchAsync(SearchQuery.All);
                StarredMails = [];
                list.ItemsSource = StarredMails;
                foreach (var id in ids)
                {
                    var message = await folder.GetMessageAsync(id);
                    Application.Current.Dispatcher.Invoke(new(() =>
                    {
                        StarredMails.Add(message);
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public ICommand SnoozedCommand { get; set; }
    public async void SnoozedCommandExecute(object? obj)
    {
        if (obj is ListView list)
        {
            try
            {
                using var imap = new ImapClient();
                await imap.ConnectAsync("imap.gmail.com", 993, true);
                await imap.AuthenticateAsync("qasimov.vaqif512@gmail.com", "aphh stkt mdoe yzxa");

                var folder = imap.GetFolder(SpecialFolder.Junk);
                await folder?.OpenAsync(FolderAccess.ReadOnly);

                var ids = await folder?.SearchAsync(SearchQuery.All);
                SnoozedMails = [];
                list.ItemsSource = SnoozedMails;
                foreach (var id in ids)
                {
                    var message = await folder.GetMessageAsync(id);
                    Application.Current.Dispatcher.Invoke(new(() =>
                        SnoozedMails.Add(message)
                    ));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public ICommand DraftCommand { get; set; }
    public async void DraftCommandExecute(object? obj)
    {
        if (obj is ListView list)
        {
            try
            {
                using var imap = new ImapClient();
                await imap.ConnectAsync("imap.gmail.com", 993, true);
                await imap.AuthenticateAsync("qasimov.vaqif512@gmail.com", "aphh stkt mdoe yzxa");

                var folder = imap.GetFolder(SpecialFolder.Drafts);
                await folder?.OpenAsync(FolderAccess.ReadOnly);

                var ids = await folder?.SearchAsync(SearchQuery.All);
                DraftMails = [];
                list.ItemsSource = DraftMails;
                foreach (var id in ids)
                {
                    var message = await folder.GetMessageAsync(id);
                    Application.Current.Dispatcher.Invoke(new(() =>
                    {
                        DraftMails.Add(message);
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }




    private async void StarredBtn_Click(object sender, RoutedEventArgs e)
    {
        await GetMailsAsync(SpecialFolder.Important);
    }

    private async void SnooozdBtn_Click(object sender, RoutedEventArgs e)
    {
        await GetMailsAsync(SpecialFolder.Archive);
    }

    private async void SentBtn_Click(object sender, RoutedEventArgs e)
    {
        await GetMailsAsync(SpecialFolder.Sent);
    }

    private async void DraftBtn_Click(object sender, RoutedEventArgs e)
    {
        await GetMailsAsync(SpecialFolder.Drafts);

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