using System;

namespace Picums.Mvc.UserAccess.Claims
{
    public struct Permission : IEquatable<Permission>
    {
        public Permission(Guid id)
        {
            this.Id = id;
        }

        public Permission(string id)
            : this(Guid.Parse(id))
        {
        }

        public Guid Id { get; }

        public static implicit operator string(Permission permission)
            => permission.ToString();

        public static implicit operator Permission(string permissionId)
            => new Permission(permissionId);

        public override string ToString()
            => Id
                .ToString()
                .Trim('{', '}')
                .ToUpper();

        public override int GetHashCode()
            => this.Id.GetHashCode();

        public override bool Equals(object obj)
            => Equals((Permission)obj);

        public bool Equals(Permission other)
            => this.Id.GetHashCode() == other.GetHashCode();
    }
}