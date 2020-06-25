using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace api.Utils
{
    public class HashUtils
    {
        public async Task HasheiaSenhaAsync(Usuario usuario, string senha){
            if(!string.IsNullOrEmpty(senha) && !string.IsNullOrWhiteSpace(senha)){
                using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                    usuario.Chave = hmac.Key;
                    usuario.Senha = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                }
            }
        }
        public void HasheiaSenha(dynamic usuario){
            if(usuario.SenhaString != null && !string.IsNullOrWhiteSpace(usuario.SenhaString)){
                using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                    usuario.Chave = hmac.Key;
                    usuario.Senha = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(usuario.SenhaString));
                }
            }
        }
        public async Task<bool> VerificaSenhaHashAsync(Usuario usuario, string senha){
            if(senha != null && !string.IsNullOrWhiteSpace(senha)){
                if(usuario.Senha.Length == 64 && usuario.Chave.Length == 128){
                    using(var hmac = new System.Security.Cryptography.HMACSHA512(usuario.Chave)){
                        var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                        for(int i = 0; i < ComputedHash.Length; i++){
                            if(ComputedHash[i] != usuario.Senha[i]){
                                return false;
                            }
                        }
                        return true;
                    }
                }else{
                    return false;
                }
            }else{
                return false;
            }
        }
        public bool VerificaSenhaHash(dynamic usuario){
            if(usuario.SenhaString != null && !string.IsNullOrWhiteSpace(usuario.Senha)){
                if(usuario.Senha.Lenght == 64 && usuario.Chave.Length == 128){
                    using(var hmac = new System.Security.Cryptography.HMACSHA512(usuario.Chave)){
                        var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(usuario.SenhaString));
                        for(int i = 0; i < ComputedHash.Length; i++){
                            if(ComputedHash[i] != usuario.Senha[i]){
                                return false;
                            }
                        }
                        return true;
                    }
                }else{
                    return false;
                }
            }else{
                return false;
            }
        }
    }
}