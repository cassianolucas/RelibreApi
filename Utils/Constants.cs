namespace RelibreApi.Utils
{
    public class Constants
    {
        public const string FormatDateTimeDefault = "dd/MM/yyyy HH:mm:ss";
        public const string Configuration = "Settings";
        public const string EmailSettings = "EmailSettings";
        public const string UserClaimIdentifier = "email_login";
        public const string GeolocationApi = "GeolocationApi";
        public const string RedirectLogin = "RedirectLogin";
        public const string RedirectError = "RedirectError";
        public const string RedirectChangePassword = "RedirectChangePassword";
        public const string EndpointImage = "EndpointImage";
        public const string AccessKeyS3 = "AccessKeyS3";
        public const string SecretKeyS3 = "SecretKeyS3";
        public const string BucketNameS3 = "BucketNameS3";
        public const string DefaultContentType = "application/json";
        public const string MessageExceptionDefault = "Não foi possível completar a requisição";
        public const string MessageExceptionConflict = "{0} Cadastro já existente!";
        public const string MessageExceptionNotAuthorize = "Não autorizado!";
        public const string Sucess = "Sucesso";
        public const string NotFound = "Não localizado!";
        public const string UserFound = "Usuário existente!";
        public const string UserNotFound = "Usuário não localizado!";
        public const string UserAddressNotFound = "Necessário cadastrar um endereço!";
        public const string UserInvalidOrPassword = "Usuário ou senha inválidos!";
        public const string UserNotValidate = "Email não verificado!";
        public const string UserPasswordInvalid = "Senha inválida";
        public const string UserDocumentInvalid = "Documento inválido";
        public const string UserLoginInvalid = "Email inválido";
        public const string Error = "Erro!";
        public const string CodeInvalid = "Código não válido!";
        public const string InvalidParameter = "Parametro não informado!";
        public const string LibraryNotFound = "Biblioteca não localizada!";
        public const string BookNotFound = "Livro não localizado!";
        public const string InvalidImage = "Nenhuma imagem localizada!";
        public const string InvalidNotification = "Notificação não localizada!";
        public const string InvalidType = "Tipo inválido!";
        public const string SpecialCharacter = @"[^\w\s]|[ºª]";
        public enum Requests { Get, Post, Put };
        public enum Types { Trocar, Doar, Emprestar, Interesse };
    }
}