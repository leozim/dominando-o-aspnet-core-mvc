using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspnetCoreMvc.Extensions;

[HtmlTargetElement("*", Attributes = "disable-claim-by-name")]
[HtmlTargetElement("*", Attributes = "disable-claim-by-value")]
public class DesabilitaLinkByTagHelper : TagHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DesabilitaLinkByTagHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    [HtmlAttributeName("disable-claim-by-name")]
    public string? IdentityClaimName { get; set; }
    
    [HtmlAttributeName("disable-by-claim-value")]
    public string? IdentityClaimValue { get; set; }

    public override void Process(
        TagHelperContext context,
        TagHelperOutput output)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (output == null) throw new ArgumentNullException(nameof(output));
        
        var temAcesso = CustomAuthorization.ValidarClaimsUsuario(
            _httpContextAccessor.HttpContext, 
            IdentityClaimName, 
            IdentityClaimValue);

        if (temAcesso) return;

        output.Attributes.RemoveAll("href");
        output.Attributes.Add(new TagHelperAttribute("style", "cursor: not-allowed"));
        output.Attributes.Add(new TagHelperAttribute("title", "Você não tem permissão"));
    }
}