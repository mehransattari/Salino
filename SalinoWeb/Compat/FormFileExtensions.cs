namespace SalinoMvc5.Compat
{
    /// <summary>HttpPostedFileBase members used by the legacy upload code.</summary>
    public static class FormFileExtensions
    {
        public static void SaveAs(this IFormFile file, string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            using var stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }
    }
}
