namespace Labb3_Gilbert
{
    public partial class MainWindow
    {
        interface IBokning
        {
            public string Name { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            public string Tablenumber { get; set; }
        }
    }
}
