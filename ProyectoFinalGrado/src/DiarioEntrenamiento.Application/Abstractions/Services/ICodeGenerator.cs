namespace DiarioEntrenamiento.Application.Abstractions.Services;

public interface ICodeGenerator
{
    (string,string) GenerateCode();
}