namespace PhoneWord;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}
    string translatedNumber;

    private void OnTranslate(object sender, EventArgs e)
    {
        string enteredNumber = PhoneNumberText.Text;
        // Invoco la clase que traduce
        translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);

        // Si no es nulo que hago lo siguiente
        if (!string.IsNullOrEmpty(translatedNumber))
        {
            CallButton.IsEnabled = true;
            CallButton.Text = "Llamar a " + translatedNumber;
        }
        // Si es nulo que haga lo siguiente
        else
        {
            CallButton.IsEnabled = false;
            CallButton.Text = "Llamar";
        }
    }
    // Evento que realiza la llamada
    async void OnCall(object sender, System.EventArgs e)
    {
        // preguntara si quiere realizar la llamada
        // Display Alert permite desplegar una ventana emergente
        // Si la tuviera esta ventana emergente solo una opcion, como por ejemplo aceptar, no devolveria ningun valor
        // Pero como la ventana emergente tiene dos opciones nos devuelve un booleano
        if (await this.DisplayAlert(
            "Marcado",
            "¿Quieres llamar al " + translatedNumber + "?",
            "Si",
            "No"))
        {
            
            // Utilizamos un try/catch por si algo falla
            try
            {
                // Antes de marcar evalua si el telefono tiene la capacidad de marcar.
                // Si puede marcar abre la opcion de marcado
                if (PhoneDialer.Default.IsSupported)
                    PhoneDialer.Default.Open(translatedNumber);
                else
                    await DisplayAlert("Error al llamar", "El dispositivo no permite llamadas", "Aceptar");
                // Como solo tiene la opcion OK no devuelve ningun valor
            }

            // Si falla avisa que no se puede marcar porque el numero no es valido
            catch (ArgumentNullException)
            {
                await DisplayAlert("Error al llamar", "El número no es valido", "Aceptar");
            }
            // Si falla avisa que no se puede marcar. Este es un error mas general
            catch (Exception)
            {
                // Other error has occurred.
                await DisplayAlert("Error al llamar", "Error desconocido", "Aceptar");
            }
        }
    }
}

