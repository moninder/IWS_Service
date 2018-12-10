using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;

namespace AirportIQ.Model.EF.SAFE
{
	public partial class SafeEntities : DbContext
	{
		public bool IsAttached(object context, EntityKey key)
		{
			var myContext = (SafeEntities)context;
			var objectStateManager = ((IObjectContextAdapter)myContext).ObjectContext.ObjectStateManager;
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}

			


			ObjectStateEntry entry;
			if (objectStateManager.TryGetObjectStateEntry(key, out entry))
			{
				return (entry.State != EntityState.Detached);
			}
			return false;
		}



	}
}

