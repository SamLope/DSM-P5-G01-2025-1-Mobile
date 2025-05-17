namespace Mobile.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}
    private void OnIniciarClicked(object sender, EventArgs e)
    {
        if (Parent is TabbedPage tabbedPage)
        {
            
            tabbedPage.CurrentPage = tabbedPage.Children[1];
        }
    }
}