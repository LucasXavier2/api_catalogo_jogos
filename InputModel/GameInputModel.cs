using System;
using System.ComponentModel.DataAnnotations;

namespace ApiCatalogoJogos.InputModel {
    public class GameInputModel {
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome do jogo deve conter entre 2 e 100 caracteres")]
        public string Name { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da companhia deve conter entre 1 e 100 caracteres")]
        public string Company { get; set; }
        
        [Required]
        [Range(0, 1000, ErrorMessage = "O preço deve ser entre 0 e 1000")]
        public double Price { get; set; }
    }
}