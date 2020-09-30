namespace RelibreApi.Utils
{
    public class Constants
    {
        public const string FormatDateTimeDefault = "dd/MM/yyyy HH:mm:ss";
        public const string Configuration = "Settings";
        public const string EmailSettings = "EmailSettings";
        public const string GeolocationApi = "GeolocationApi";
        public const string DefaultContentType = "application/json";
        public const string MessageExceptionDefault = "Não foi possível completar a requisição";
        public const string MessageExceptionConflict = "{0} Cadastro já existente!";
        public const string MessageExceptionNotAuthorize = "Não autorizado!";
        public const string SpecialCharacter = @"[^\w\s]|[ºª]";
        public enum Requests { Get, Post, Put };
        public enum Types { Trocar, Doar, Emprestar, Interesse };
    }
}