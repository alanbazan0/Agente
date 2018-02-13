using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml.Automation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NavigationMenuSample.Controls;
using NavigationMenuSample.Views;
using AgenteApp.UWP;
using Windows.UI.ViewManagement;
using Windows.UI;
using AgenteApp.Modelos;
using AgenteApp.Clases;

//usings Linphone
using Linphone;
using Windows.System.Threading;
using Windows.ApplicationModel.Core;
using AgenteApp.UWP.Vistas;
using AgenteApp.Vistas;
using AgenteApp.Presentadores;
using System.IO;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;

namespace NavigationMenuSample
{
    /// <summary>
    /// The "chrome" layer of the app that provides top-level navigation with
    /// proper keyboarding navigation.
    /// </summary>
    public sealed partial class AppShell :  Page,IAppShell
    {
        private bool isPaddingAdded = false;
        // Declare the top level nav items
        private List<NavMenuItem> navlist = new List<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    Symbol = Symbol.Phone,
                    Image="../Assets/img1.png",
                    Label = "llamada entrante",
                    DestPage = typeof(RecepcionLlamadaPage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.AddFriend,
                    Image="../Assets/img2.png",
                    Label = "Identificación del cliente",
                    DestPage = typeof(ClientesDetallePage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Contact,
                    Image="../Assets/img3.png",
                    Label = "Interacciones (CRM)",
                    DestPage = typeof(CRMPage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Contact,
                    Image="../Assets/img4.png",
                    Label = "Tipificación",
                    DestPage = typeof(TipificacionPage)
                },
                //new NavMenuItem()
                //{
                //    Symbol = Symbol.BackToWindow,
                //    Label = "Tranferir",
                //    DestPage = typeof(TransferenciaPage)
                //},
                //new NavMenuItem()
                //{
                //    Symbol = Symbol.BackToWindow,
                //    Label = "Conferenciar",
                //    DestPage = typeof(BasicPage)
                //},
                new NavMenuItem()
                {
                    Symbol = Symbol.Mail,
                    Image="../Assets/im5.png",
                    Label = "Correo electrónico",
                    DestPage = typeof(CorreoPage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Send,
                    Image="../Assets/im6.png",
                    Label = "SMS",
                    DestPage = typeof(SmsPage)
                },
                /*new NavMenuItem()
                {
                    Symbol = Symbol.Contact,
                    Label = "Queja",
                    DestPage = typeof(QuejasPage)
                },*/
                new NavMenuItem()
                {
                    Symbol = Symbol.BackToWindow,
                    Image="../Assets/im7.png",
                    Label = "Call back",
                    DestPage = typeof(BasicPage)
                },             
                new NavMenuItem()
                {
                    Symbol = Symbol.Contact,
                    Image="../Assets/im8.png",
                    Label = "Llamar(Predictivo/CallBack)",
                    DestPage = typeof(LlamarPredictivoPage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Contact,
                    Image="../Assets/im9.png",
                    Label = "Llamar(Manualmente)",
                    DestPage = typeof(LlamarManualmentePage)
                },
                 new NavMenuItem()
                {
                    Symbol = Symbol.Contact,
                    Image="../Assets/im10.png",
                    Label = "Chat interno",
                    DestPage = typeof(BasicPage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Contact,
                    Image="../Assets/im11.png",
                    Label = "Receso",
                    DestPage = typeof(RecesoPage)
                },
                /* new NavMenuItem()
                {
                    Symbol = Symbol.Contact,
                    Image="../Assets/im12.png",
                    Label = "Reconocimiento de voz",
                    DestPage = typeof(ReconocimientoVozPage)
                },*/
                new NavMenuItem()
                {
                    Symbol = Symbol.Contact,
                    Image="../Assets/im12.png",
                    Label = "Calidad",
                    DestPage = typeof(BasicPage)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Contact,
                    Image="../Assets/im13.png",
                    Label = "Asistencias y permisos",
                    DestPage = typeof(BasicPage)
                },
                 new NavMenuItem()
                {
                    Symbol = Symbol.Contact,
                    Image="../Assets/im14.png",
                    Label = "Cerrar sesion",
                    DestPage = typeof(CerrarSesionPage)
                }
            });

        public static AppShell Current = null;
        private static Usuario usa;
        public AppShellPresentador presentador;
        public Boolean entro=false;
        private DispatcherTimer tiempoSesion;
        int seg = 0;
        int segAcum = 0;
        int segConex = 0;
        int min = 0;
        int minAcum = 0;
        int minConex = 0;
        int hor = 0;
        int horAcum = 0;
        int horConex = 0;
        int segunds = 0;
        int segundos = 0;
        /// <summary>
        /// Initializes a new instance of the AppShell, sets the static 'Current' reference,
        /// adds callbacks for Back requests and changes in the SplitView's DisplayMode, and
        /// provide the nav menu list with the data to display.
        /// </summary>
        /// 


        public AppShell()
        {            
            
            this.InitializeComponent();
            
            this.Loaded += (sender, args) =>
            {
                Current = this;

                this.CheckTogglePaneButtonSizeChanged();

                var titleBar = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().TitleBar;
                titleBar.IsVisibleChanged += TitleBar_IsVisibleChanged;
            };

            this.RootSplitView.RegisterPropertyChangedCallback(
                SplitView.DisplayModeProperty,
                (s, a) =>
                {
                    // Ensure that we update the reported size of the TogglePaneButton when the SplitView's
                    // DisplayMode changes.
                    this.CheckTogglePaneButtonSizeChanged();
                });

            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;


            NavMenuList.ItemsSource = navlist;


            tiempoSesion = new DispatcherTimer();
            tiempoSesion.Tick += dispatcherTimer_Tick2;
            tiempoSesion.Interval = new TimeSpan(0, 0, 1);
            tiempoSesion.Start();

        }
        void dispatcherTimer_Tick2(object sender, object e)
        {
            mostrarTime();
        }
        private void mostrarTime()
        {
            if (minAcum == 60)
            {
                horAcum += 1;
                minAcum = 0;
            }
            if (segAcum == 60)
            {
                minAcum += 1;
                segAcum = 0;
            }
            segAcum += 1;
            String Sseg = "0";
            if (segAcum < 10)
            { Sseg += segAcum.ToString(); }
            else
            {
                Sseg = segAcum.ToString();
            }
            String Smin = "0";
            if (minAcum < 10)
            { Smin += minAcum.ToString(); }
            else
            {
                Smin = minAcum.ToString();
            }
            String Shor = "0";
            if (horAcum < 10)
            { Shor += horAcum.ToString(); }
            else
            {
                Shor = horAcum.ToString();
            }
            Tiempo.Text = Shor + ":" + Smin + ":" + Sseg;
        }
        public Frame AppFrame { get { return this.frame; } }

        /// <summary>
        /// Invoked when window title bar visibility changes, such as after loading or in tablet mode
        /// Ensures correct padding at window top, between title bar and app content
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TitleBar_IsVisibleChanged(Windows.ApplicationModel.Core.CoreApplicationViewTitleBar sender, object args)
        {
            if (!this.isPaddingAdded && sender.IsVisible)
            {
                //add extra padding between window title bar and app content
                double extraPadding = (Double)App.Current.Resources["DesktopWindowTopPadding"];
                this.isPaddingAdded = true;

                Thickness margin = NavMenuList.Margin;
                NavMenuList.Margin = new Thickness(margin.Left, margin.Top + extraPadding, margin.Right, margin.Bottom);
                margin = AppFrame.Margin;
                AppFrame.Margin = new Thickness(margin.Left, margin.Top + extraPadding, margin.Right, margin.Bottom);
                margin = TogglePaneButton.Margin;
                TogglePaneButton.Margin = new Thickness(margin.Left, margin.Top + extraPadding, margin.Right, margin.Bottom);

            }
        }
        //metodo donde viene a caer despues del llamado de Frame.Navegate
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // This just gets in the way.
                //this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            // Change minimum window size
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(320, 200));

            // Darken the window title bar using a color value to match app theme
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            if (titleBar != null)
            {
                Color titleBarColor = (Color)App.Current.Resources["SystemChromeMediumColor"];
                titleBar.BackgroundColor = titleBarColor;
                titleBar.ButtonBackgroundColor = titleBarColor;
            }

            if (SystemInformationHelpers.IsTenFootExperience)
            {
                // Apply guidance from https://msdn.microsoft.com/windows/uwp/input-and-devices/designing-for-tv
                ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);

                this.Resources.MergedDictionaries.Add(new ResourceDictionary
                {
                    Source = new Uri("ms-appx:///Styles/TenFootStylesheet.xaml")
                });
            }

            AppShell shell = Window.Current.Content as AppShell;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (shell == null)
            {
                // Create a AppShell to act as the navigation context and navigate to the first page
                shell = new AppShell();

                // Set the default language
                shell.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                shell.AppFrame.NavigationFailed += OnNavigationFailed;

                //if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                //{
                //    //TODO: Load state from previously suspended application
                //}
            }

            // Place our app shell in the current Window
            Window.Current.Content = shell;
            Usuario usuario = (Usuario)e.Parameter;
            usa = usuario;
            if (shell.AppFrame.Content == null)
            {
                // When the navigation stack isn't restored, navigate to the first page
                // suppressing the initial entrance animation.
                shell.AppFrame.Navigate(typeof(RecepcionLlamadaPage), usuario, new Windows.UI.Xaml.Media.Animation.SuppressNavigationTransitionInfo());
            }

            
            // Ensure the current window is active
            Window.Current.Activate();
            /*Nombre.Text = usa.Nombre;
            if(usa.Image!=null)
                Foto.Source = usa.Image;
            Tiempo.Text = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("%MM") + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            */
        }



        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Default keyboard focus movement for any unhandled keyboarding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppShell_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            FocusNavigationDirection direction = FocusNavigationDirection.None;
            switch (e.Key)
            {
                case Windows.System.VirtualKey.Left:
                case Windows.System.VirtualKey.GamepadDPadLeft:
                case Windows.System.VirtualKey.GamepadLeftThumbstickLeft:
                case Windows.System.VirtualKey.NavigationLeft:
                    direction = FocusNavigationDirection.Left;
                    break;
                case Windows.System.VirtualKey.Right:
                case Windows.System.VirtualKey.GamepadDPadRight:
                case Windows.System.VirtualKey.GamepadLeftThumbstickRight:
                case Windows.System.VirtualKey.NavigationRight:
                    direction = FocusNavigationDirection.Right;
                    break;

                case Windows.System.VirtualKey.Up:
                case Windows.System.VirtualKey.GamepadDPadUp:
                case Windows.System.VirtualKey.GamepadLeftThumbstickUp:
                case Windows.System.VirtualKey.NavigationUp:
                    direction = FocusNavigationDirection.Up;
                    break;

                case Windows.System.VirtualKey.Down:
                case Windows.System.VirtualKey.GamepadDPadDown:
                case Windows.System.VirtualKey.GamepadLeftThumbstickDown:
                case Windows.System.VirtualKey.NavigationDown:
                    direction = FocusNavigationDirection.Down;
                    break;
            }

            if (direction != FocusNavigationDirection.None)
            {
                var control = FocusManager.FindNextFocusableElement(direction) as Control;
                if (control != null)
                {
                    control.Focus(FocusState.Keyboard);
                    e.Handled = true;
                }
            }
        }

        #region BackRequested Handlers

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            bool handled = e.Handled;
            this.BackRequested(ref handled);
            e.Handled = handled;
        }

        private void BackRequested(ref bool handled)
        {
            // Get a hold of the current frame so that we can inspect the app back stack.

            if (this.AppFrame == null)
                return;

            // Check to see if this is the top-most page on the app back stack.
            if (this.AppFrame.CanGoBack && !handled)
            {
                // If not, set the event to handled and go back to the previous page in the app.
                handled = true;
                this.AppFrame.GoBack();
            }
        }

        #endregion

        #region Navigation

        /// <summary>
        /// Navigate to the Page for the selected <paramref name="listViewItem"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="listViewItem"></param>
        private void NavMenuList_ItemInvoked(object sender, ListViewItem listViewItem)
        {
            foreach (var i in navlist)
            {
                i.IsSelected = false;
            }
            
            var item = (NavMenuItem)((NavMenuListView)sender).ItemFromContainer(listViewItem);
            item.Arguments = usa;
            if (item != null)
            {
                item.IsSelected = true;
                if (item.DestPage != null &&
                    item.DestPage != this.AppFrame.CurrentSourcePageType)
                {
                    switch (item.Label)
                    {
                        case "Identificación del cliente":
                            var parametros = new { modo = ModoVentana.ALTA, telCliente = "8711897006", usuarioId = usa.Id/*, idCliente = idCliente, portabilidad = portabilidadParametros*/ };
                            item.Arguments = parametros;
                            this.AppFrame.Navigate(item.DestPage, item.Arguments);
                            break;
                        case "Receso":
                            item.Arguments = usa;
                            this.AppFrame.Navigate(item.DestPage, item.Arguments);
                            break;
                        default:
                            this.AppFrame.Navigate(item.DestPage, item.Arguments);
                            break;
                    }

                    //Page destPage = (Page) Activator.CreateInstance(item.DestPage);
                    //this.AppFrame.Content = destPage;
                    String a = this.TogglePaneButton.IsChecked.ToString();

                    /*Nombre.Text = usa.Nombre;
                    Tiempo.Text= DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("%MM") + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    if (usa.Image != null)
                        Foto.Source = usa.Image;
                    Tiempo.Text = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("%MM") + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    */

                    CloseNavePane();

                  
                }
            }
            
        }

        /// <summary>
        /// Ensures the nav menu reflects reality when navigation is triggered outside of
        /// the nav menu buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNavigatingToPage(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                var item = (from p in this.navlist where p.DestPage == e.SourcePageType select p).SingleOrDefault();
                if (item == null && this.AppFrame.BackStackDepth > 0)
                {
                    // In cases where a page drills into sub-pages then we'll highlight the most recent
                    // navigation menu item that appears in the BackStack
                    foreach (var entry in this.AppFrame.BackStack.Reverse())
                    {
                        item = (from p in this.navlist where p.DestPage == entry.SourcePageType select p).SingleOrDefault();
                        if (item != null)
                            break;
                    }
                }

                foreach (var i in navlist)
                {
                    i.IsSelected = false;
                }
                if (item != null)
                {
                    item.IsSelected = true;
                }

                var container = (ListViewItem)NavMenuList.ContainerFromItem(item);

                // While updating the selection state of the item prevent it from taking keyboard focus.  If a
                // user is invoking the back button via the keyboard causing the selected nav menu item to change
                // then focus will remain on the back button.
                if (container != null) container.IsTabStop = false;
                NavMenuList.SetSelectedItem(container);
                if (container != null) container.IsTabStop = true;


               /* Nombre.Text = usa.Nombre;
                if (usa.Image != null)
                    Foto.Source = usa.Image;
                Tiempo.Text = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("%MM") + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
               */

            }
        }

        #endregion

        public Rect TogglePaneButtonRect
        {
            get;
            private set;
        }
        public object Base64 { get; private set; }
        public object PixelFormats { get; private set; }

        /// <summary>
        /// An event to notify listeners when the hamburger button may occlude other content in the app.
        /// The custom "PageHeader" user control is using this.
        /// </summary>
        public event TypedEventHandler<AppShell, Rect> TogglePaneButtonRectChanged;

        /// <summary>
        /// Public method to allow pages to open SplitView's pane.
        /// Used for custom app shortcuts like navigating left from page's left-most item
        /// </summary>
        public void OpenNavePane()
        {
            TogglePaneButton.IsChecked = true;
            NavPaneDivider.Visibility = Visibility.Visible;

            /*Nombre.Text = usa.Nombre;
            if (usa.Image != null)
                Foto.Source = usa.Image;
            Tiempo.Text = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("%MM") + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            */
        }
        public void CloseNavePane()
        {
            TogglePaneButton.IsChecked = false;
            NavPaneDivider.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Hides divider when nav pane is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void RootSplitView_PaneClosed(SplitView sender, object args)
        {
            NavPaneDivider.Visibility = Visibility.Collapsed;

            // Prevent focus from moving to elements when they're not visible on screen
            FeedbackNavPaneButton.IsTabStop = false;
            SettingsNavPaneButton.IsTabStop = false;
        }

        /// <summary>
        /// Callback when the SplitView's Pane is toggled closed.  When the Pane is not visible
        /// then the floating hamburger may be occluding other content in the app unless it is aware.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TogglePaneButton_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CheckTogglePaneButtonSizeChanged();
        }

        /// <summary>
        /// Callback when the SplitView's Pane is toggled opened.
        /// Restores divider's visibility and ensures that margins around the floating hamburger are correctly set.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TogglePaneButton_Checked(object sender, RoutedEventArgs e)
        {
            NavPaneDivider.Visibility = Visibility.Visible;
            this.CheckTogglePaneButtonSizeChanged();

            FeedbackNavPaneButton.IsTabStop = true;
            SettingsNavPaneButton.IsTabStop = true;
        }

        /// <summary>
        /// Check for the conditions where the navigation pane does not occupy the space under the floating
        /// hamburger button and trigger the event.
        /// </summary>
        private void CheckTogglePaneButtonSizeChanged()
        {
            if (this.RootSplitView.DisplayMode == SplitViewDisplayMode.Inline ||
                this.RootSplitView.DisplayMode == SplitViewDisplayMode.Overlay)
            {
                var transform = this.TogglePaneButton.TransformToVisual(this);
                var rect = transform.TransformBounds(new Rect(0, 0, this.TogglePaneButton.ActualWidth, this.TogglePaneButton.ActualHeight));
                this.TogglePaneButtonRect = rect;
            }
            else
            {
                this.TogglePaneButtonRect = new Rect();
            }

            var handler = this.TogglePaneButtonRectChanged;
            if (handler != null)
            {
                // handler(this, this.TogglePaneButtonRect);
                handler.DynamicInvoke(this, this.TogglePaneButtonRect);
            }

        }

        /// <summary>
        /// Enable accessibility on each nav menu item by setting the AutomationProperties.Name on each container
        /// using the associated Label of each item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NavMenuItemContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            presentador = new AppShellPresentador(this);
            if (!args.InRecycleQueue && args.Item != null && args.Item is NavMenuItem)
            {
                args.ItemContainer.SetValue(AutomationProperties.NameProperty, ((NavMenuItem)args.Item).Label);
            }
            else
            {
                args.ItemContainer.ClearValue(AutomationProperties.NameProperty);
            }
            Nombre.Text = usa.Nombre;

            if (!entro)
            {
                entro = true;
                if (usa.Image != null)
                    Foto.Source = usa.Image;
                else
                    presentador.consultarFotoUsuario(usa.Id);                
            }

            int mes = DateTime.Now.Month;
            string mesS = "";
            if (mes < 9)
                mesS = "0" + mes;
            else
                mesS = ""+ mes;

            int dia = DateTime.Now.Day;
            string diaS = "";
            if (dia < 9)
                diaS = "0" + dia;
            else
                diaS = "" + dia;
            FechaI.Text = DateTime.Now.Year.ToString() +"/"+ mesS + "/"+ diaS;

        }

        public async void mostrarFotoUsuarioAsync(string imagen)
        {
            var bytes = Convert.FromBase64String(imagen);
             var buf = bytes.AsBuffer();
             var stream = buf.AsStream();    

            var image = stream.AsRandomAccessStream();

            var decoder = await BitmapDecoder.CreateAsync(image);
            image.Seek(0);

            var output = new WriteableBitmap((int)decoder.PixelHeight, (int)decoder.PixelWidth);
            await output.SetSourceAsync(image);
            Foto.Source=output;
        }
    }
}
