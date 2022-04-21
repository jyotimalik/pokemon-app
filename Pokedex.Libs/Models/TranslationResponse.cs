namespace Pokedex.Libs.Models
{
    public class TranslationResponse
    {
        public TranslationSuccess Success { get; set; }
        public TranslationContent Contents { get; set; }
    }

   public class TranslationContent
   {
       public string Translated { get; set; }
       public string Text { get; set; }
       public string Translation { get; set; }
   }
   public class TranslationSuccess
   {
       public int Total { get; set; }
     
   }
}
