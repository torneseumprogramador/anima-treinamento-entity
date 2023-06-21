using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entity.Patterns;

public record CPF
{
    private string cpf;
    public CPF(string cpf)
    {
        this.validaCPF(cpf);
        this.cpf = cpf;
    }

    public override string ToString()
    {
        return this.cpf;
    }

    private void validaCPF(string cpf)
    {
        if(cpf is null) throw new Exception("CPF não pode ser null");

		int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
		int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
		string tempCpf;
		string digito;
		int soma;
		int resto;
		cpf = cpf.Trim();
		cpf = cpf.Replace(".", "").Replace("-", "");
		if (cpf.Length != 11) throw new Exception("CPF não pode ter menos que 11 caracteres");
		
		if (cpf == "00000000000") throw new Exception("CPF inválido");
		if (cpf == "11111111111") throw new Exception("CPF inválido");
		if (cpf == "22222222222") throw new Exception("CPF inválido");
		if (cpf == "33333333333") throw new Exception("CPF inválido");
		if (cpf == "44444444444") throw new Exception("CPF inválido");
		if (cpf == "55555555555") throw new Exception("CPF inválido");
		if (cpf == "66666666666") throw new Exception("CPF inválido");
		if (cpf == "77777777777") throw new Exception("CPF inválido");
		if (cpf == "88888888888") throw new Exception("CPF inválido");
		if (cpf == "99999999999") throw new Exception("CPF inválido");
		
		tempCpf = cpf.Substring(0, 9);
		soma = 0;

		for(int i=0; i<9; i++)
		    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
		resto = soma % 11;
		if ( resto < 2 )
		    resto = 0;
		else
		   resto = 11 - resto;
		digito = resto.ToString();
		tempCpf = tempCpf + digito;
		soma = 0;
		for(int i=0; i<10; i++)
		    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
		resto = soma % 11;
		if (resto < 2)
		   resto = 0;
		else
		   resto = 11 - resto;
		digito = digito + resto.ToString();
		if(!cpf.EndsWith(digito))
            throw new Exception("CPF é inválido");
    }
}