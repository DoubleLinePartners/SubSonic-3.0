using Castle.MicroKernel.Registration;
using Castle.Windsor;
using SubSonic.TypeConverters;

namespace SubSonic.Configuration
{
    public class Configurator
    {
        private static volatile Configurator _instance;
        private static readonly object SyncRoot = new object();
        private IWindsorContainer _container;

        private Configurator ()
        {
            InitializeContainer();
        }

        public static IWindsorContainer Container
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new Configurator();
                        }
                    }
                }
                return _instance._container;
            }
        }

        private void InitializeContainer()
        {
            var container = new WindsorContainer();
            _container =
                container.Register(
                    AllTypes.FromThisAssembly()
                            .BasedOn(typeof (IValueTypeConverter<,>))
                            .WithServiceAllInterfaces()
                            .LifestyleTransient());
        }
    }
}