﻿using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estoque.Domain.Core.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        [NotMapped]
        public ValidationResult ValidationResult { get; protected set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
            ValidationResult = new ValidationResult();
        }

        public void AddValidationError(string error, string description)
        {
            ValidationResult.Errors.Add(new ValidationFailure(error, description));
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
