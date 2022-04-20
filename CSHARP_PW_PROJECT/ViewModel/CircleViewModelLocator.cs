using CommonServiceLocator;
using CSHARP_PW_PROJECT.ViewModel;
using GalaSoft.MvvmLight.Ioc;

namespace PRESENTATION_LAYER.ViewModel

{
    internal class CircleViewModelLocator
    {
        public CircleViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<CircleViewModel>();

        }

        public CircleViewModel CircleViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CircleViewModel>();
            }
        }


    }
}
