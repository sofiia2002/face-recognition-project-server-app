using System;
using System.ComponentModel.DataAnnotations;

namespace FaceRecognitionServer.Domain.Models
{
    public class PersonViewModel
    {
        public PersonViewModel() { }
        public PersonViewModel(Guid id) 
        {
            id = id;
        }

        public PersonViewModel(string name, string description, Guid id)
        {
            Name = name;
            Description = description;
            Id = id;
        }

        [StringLength(64)]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [StringLength(64)]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "URL is required")]
        public string ImageUrl { get; set; }
        public Guid Id { get; set; }
        public double? Confidence { get; set; }
    }
}
