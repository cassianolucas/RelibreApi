namespace RelibreApi.Utils
{
    public class Constants
    {
        public const string FormatDateTimeDefault = "dd/MM/yyyy HH:mm:ss";
        public const string Configuration = "Settings";
        public const string EmailSettings = "EmailSettings";
        public const string HtmlSettings = "HtmlSettings";
        public const string UserClaimIdentifier = "email_login";
        public const string GeolocationApi = "GeolocationApi";
        public const string RedirectLogin = "RedirectLogin";
        public const string RedirectLoginBusiness = "RedirectLoginBusiness";
        public const string RedirectError = "RedirectError";
        public const string RedirectChangePassword = "RedirectChangePassword";
        public const string EndpointImage = "EndpointImage";
        public const string AccessKeyS3 = "AccessKeyS3";
        public const string SecretKeyS3 = "SecretKeyS3";
        public const string BucketNameS3 = "BucketNameS3";
        public const string DefaultContentType = "application/json";
        public const string SpecialCharacter = @"[^\w\s]|[ºª]";
        public const string HtmlEmailName = "Olá, {0}";
        public const string HtmlEmailDescriptionPassword = "Recebemos uma solicitação para redefinir a senha da sua conta.";
        public const string HtmlEmailDescriptionAccount = "Seja bem vindo a nossa comunidade!";
        public const string HtmlEmailInformationPassword = "Para redefinir sua senha, clique no botão abaixo e siga as instruções.";
        public const string HtmlEmailInformationAccount = "Para confirmar seu cadastro, clique no botão abaixo.";
        public const string HtmlEmailButtonTextPassword = "Trocar senha";
        public const string HtmlEmailButtonTextAccount = "Confirmar minha conta";
        public enum HtmlEmailType { ForgetPassword, NewAccount };
        public static int[] Multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        public static int[] Multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        


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
        public const string UserCnpjInvalid = "Cnpj inválido";
        public const string UserLoginInvalid = "Email inválido";
        public const string UserNameInvalid = "Nome inválido";
        public const string UserPhoneInvalid = "Telefone inválido";
        public const string UserPlanInvalid = "Usuario não está com o plano ativo!";
        public const string Error = "Erro!";
        public const string CodeInvalid = "Código não válido!";
        public const string InvalidParameter = "Parametro não informado!";
        public const string LibraryNotFound = "Biblioteca não localizada!";        
        public const string BooksNotFound = "Nenhum livro localizado!";
        public const string BookInvalid = "Não é possível adicionar o livro da sua biblioteca!";
        public const string InvalidImage = "Nenhuma imagem localizada!";
        public const string InvalidNotification = "Notificação não localizada!";
        public const string InvalidType = "Tipo inválido!";
        public const string InvalidPlan = "Nenhum plano localizado!";
        public const string SubscriptionInvalid = "Usuário já possui um plano!";
        

    }
}