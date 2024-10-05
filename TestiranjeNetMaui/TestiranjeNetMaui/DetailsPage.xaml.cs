namespace TestiranjeNetMaui;

public partial class DetailsPage : ContentPage
{
    public DetailsPage(KolesaViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}