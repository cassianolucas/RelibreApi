namespace RelibreApi.Utils
{
    public class Constants
    {
        public const string FormatDateTimeDefault = "dd/MM/yyyy HH:mm:ss";
        public const string Configuration = "Settings";
        public const string DefaultContentType = "application/json";
        public const string MessageExceptionDefault = "Não foi possível completar a requisição";
        public const string MessageExceptionConflict = "{0} Cadastro já existente!";
        public const string MessageExceptionNotAuthorize = "Não autorizado!";
        public const string SpecialCharacter = @"[^\w\s]|[ºª]";
    }
}