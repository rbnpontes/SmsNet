using SmsNet.Core.Common;
using SmsNet.Core.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core
{
    public sealed class Context
    {
		private HashMap<IFactory> mFactories = new HashMap<IFactory>();
		private EventManager mEventManager = new EventManager();

		public static Context Singleton { get; } = new Context();


		public Context RegisterFactory<T>()
		{
			return RegisterFactory(typeof(T));
		}
		public Context RegisterFactory(Type type)
		{
			uint hash = Hashing.SDBM(type.Name);
			if (mFactories.HasKey(hash))
				throw new Exception($"Can't register '{type.Name}' as factory, because its already registred!");
			/// Instantiate and Factory
			try
			{
				IFactory factory = (IFactory)Activator.CreateInstance(type);
				mFactories[hash] = factory;

				factory.OnCreateFactory();
			}catch(InvalidCastException e)
			{
				throw new Exception("Cannot Register this Factory, because current type not Inherit IFactory Interface", e);
			}
			return this;
		}
		public Context RegisterFactory(IFactory factory)
		{
			uint hash = Hashing.SDBM(factory.GetType().Name);
			if(mFactories.HasKey(hash))
				throw new Exception($"Can't register '{factory.GetType().Name}' as factory, because its already registred!");

			mFactories[hash] = factory;
			factory.OnCreateFactory();

			return this;
		}
		public T GetSubsystem<T>()
		{
			return (T)GetSubsystemByType(typeof(T));
		}
		public object GetSubsystemByType(Type type)
		{
			uint hash = Hashing.SDBM(type.Name);
			if (!mFactories.HasKey(hash))
				throw new NullReferenceException($"Can't get subsystem '{type.Name}' because is not registred, do you use RegisterFactory ?");
			return mFactories[hash];
		}

		public Context AddListener(string listener, EventHandler callback)
		{
			mEventManager.AddListener(listener,callback);
			return this;
		}
		public Context AddListener(uint code, EventHandler callback)
		{
			mEventManager.AddListener(code, callback);
			return this;
		}
		#region SendEvent Method
		public Context SendEvent(string listener)
		{
			mEventManager.Invoke(listener, null, new object[0]);
			return this;
		}
		public Context SendEvent(string listener, params object[] parameters)
		{
			mEventManager.Invoke(listener, null, parameters);
			return this;
		}
		public Context SendEvent(object sender, string listener)
		{
			mEventManager.Invoke(listener, sender, new object[0]);
			return this;
		}
		public Context SendEvent(object sender, string listener,params object[] parameters)
		{
			mEventManager.Invoke(listener, sender, parameters);
			return this;
		}
		public Context SendEvent(uint code)
		{
			mEventManager.Invoke(code, null, new object[0]);
			return this;
		}
		public Context SendEvent(uint code, params object[] parameters)
		{
			mEventManager.Invoke(code, null, parameters);
			return this;
		}
		public Context SendEvent(object sender, uint code)
		{
			mEventManager.Invoke(code, sender, new object[0]);
			return this;
		}
		public Context SendEvent(object sender, uint code, params object[] parameters)
		{
			mEventManager.Invoke(code, sender, parameters);
			return this;
		}
		#endregion
	}
}
