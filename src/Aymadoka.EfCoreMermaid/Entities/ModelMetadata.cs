using System.Collections.Generic;

namespace Aymadoka.EfCoreMermaid.Entities
{
    public class ModelMetadata
    {
        public List<EntityMetadata> Entities { get; private set; }

        public List<RelationshipMetadata> Relationships { get; private set; }

        public ModelMetadata()
        {
            Entities = new List<EntityMetadata>();
            Relationships = new List<RelationshipMetadata>();
        }

        public void AddEntity(EntityMetadata entity)
        {
            Entities.Add(entity);
        }

        public void AddRelationship(RelationshipMetadata relationship)
        {
            Relationships.Add(relationship);
        }
    }
}
