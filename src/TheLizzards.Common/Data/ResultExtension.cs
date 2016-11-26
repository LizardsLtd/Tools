namespace TheLizzards.Common.Data
{
	public static class ResultExtension
	{
		public static IResult ToResult(this bool isSucess, string messageWhenFalse)
			=> isSucess
				? Results.Success
				: Results.Fail(messageWhenFalse);
	}
}