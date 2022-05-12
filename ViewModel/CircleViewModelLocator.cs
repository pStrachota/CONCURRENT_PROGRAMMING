using CommonServiceLocator;

using GalaSoft.MvvmLight.Ioc;

namespace ViewModel

{
    public class CircleViewModelLocator
    {
        public CircleViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<CircleViewModel>();

        }

        public CircleViewModel CircleViewModel => ServiceLocator.Current.GetInstance<CircleViewModel>();
    }
}
