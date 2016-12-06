using System;
using TheLizzards.Data.CQRS.Contracts.Data;

namespace TheLizzards.CQRS.Entities
{
	public abstract class DataContextOperatorBase : IDisposable
	{
		protected readonly IDataContext storageContext;

		private bool disposedValue;

		protected DataContextOperatorBase(IDataContext storageContext)
		{
			this.storageContext = storageContext;
		}

		public void Dispose() => Dispose(true);

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					this.storageContext?.Dispose();
				}

				disposedValue = true;
			}
		}
	}
}