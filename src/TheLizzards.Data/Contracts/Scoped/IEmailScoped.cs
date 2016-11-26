using System;
using TheLizzards.Common.DataParts.Entites;

namespace TheLizzards.Common.Services.Scoped
{
	public interface IEmailScoped<T> : IDisposable
	{
		T SetEmail(Email email);
	}
}