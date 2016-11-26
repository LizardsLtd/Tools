namespace TheLizzards.Mvc.View
{
	public static class Names
	{
		public static string DropController(this string controller)
			=> controller.Replace("Controller", "");
	}
}