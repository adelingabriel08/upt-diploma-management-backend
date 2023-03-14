using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UPT.Diploma.Management.Application.Commands.Base;
using UPT.Diploma.Management.Application.Constants;
using UPT.Diploma.Management.Application.Exceptions;
using UPT.Diploma.Management.Application.Options;
using UPT.Diploma.Management.Domain.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace UPT.Diploma.Management.Application.Commands.LoginCommand;

public class LoginCmdHandler : IRequestHandler<LoginCmd, TokenResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtOptions _jwtOptions;

    public LoginCmdHandler(UserManager<ApplicationUser> userManager, IOptions<JwtOptions> jwtOptions)
    {
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<TokenResult> Handle(LoginCmd request, CancellationToken cancellationToken)
    {
        var validator = new LoginCmdValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (validatorResult.Errors.Count > 0)
        {
            throw new ValidationException(validatorResult);
        }
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            throw new ValidationException("User does not exist or wrong password!");

        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isValidPassword)
            throw new ValidationException("User does not exist or wrong password!");
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
        var claims = new List<Claim>
        {
            new Claim(CustomClaims.UserId, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName)
        };
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new TokenResult() { Token = tokenHandler.WriteToken(token), Success = true };
    }
}