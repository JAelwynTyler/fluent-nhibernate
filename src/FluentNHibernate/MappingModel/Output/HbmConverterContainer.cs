using System;
using FluentNHibernate.Infrastructure;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Identity;
using NHibernate.Cfg.MappingSchema;

namespace FluentNHibernate.MappingModel.Output
{
    public class HbmConverterContainer : Container
    {
        public HbmConverterContainer()
        {
            Register<IHbmConverterServiceLocator>(c =>
                new HbmConverterServiceLocator(this));

            RegisterConverter<HibernateMapping, HbmMapping>(c =>
                new HbmHibernateMappingConverter(c.Resolve<IHbmConverterServiceLocator>()));

            RegisterConverter<ClassMapping, HbmClass>(c =>
                new HbmClassConverter(c.Resolve<IHbmConverterServiceLocator>()));

            RegisterConverter<ImportMapping, HbmImport>(c =>
                new HbmImportConverter());

            /*
            RegisterConverter<PropertyMapping, HbmProperty>(c =>
                new HbmPropertyConverter(c.Resolve<IHbmConverterServiceLocator>()));
            */

            RegisterIdConverters();
            RegisterComponentConverters();

            /*
            RegisterConverter<NaturalIdMapping, HbmNaturalId>(c =>
                new HbmNaturalIdConverter(c.Resolve<IHbmConverterServiceLocator>()));
            */

            RegisterConverter<ColumnMapping, HbmColumn>(c =>
                new HbmColumnConverter());
            
            /*
            RegisterConverter<JoinMapping, HbmJoin>(c =>
                new HbmJoinConverter(c.Resolve<IHbmConverterServiceLocator>()));

            RegisterConverter<DiscriminatorMapping, HbmDiscriminator>(c =>
                new HbmDiscriminatorConverter(c.Resolve<IHbmConverterServiceLocator>()));

            RegisterConverter<KeyMapping, HbmKey>(c =>
                new HbmKeyConverter(c.Resolve<IHbmConverterServiceLocator>()));

            RegisterConverter<ParentMapping, HbmParent>(c =>
                new HbmParentConverter());

            RegisterConverter<CompositeElementMapping, HbmCompositeElement>(c =>
                new HbmCompositeElementConverter(c.Resolve<IHbmConverterServiceLocator>()));

            RegisterConverter<VersionMapping, HbmVersion>(c =>
                new HbmVersionConverter(c.Resolve<IHbmConverterServiceLocator>()));

            RegisterConverter<CacheMapping, HbmCache>(c =>
                new HbmCacheConverter());

            RegisterConverter<OneToOneMapping, HbmOneToOne>(c =>
                new HbmOneToOneConverter());

            // collections
            // FIXME: What does this need to convert as?
            RegisterConverter<CollectionMapping>(c =>
                new HbmCollectionConverter(c.Resolve<IHbmConverterServiceLocator>()));

            // FIXME: What does this need to convert as?
            RegisterConverter<IIndexMapping>(c =>
                new HbmIIndexConverter(c.Resolve<IHbmConverterServiceLocator>()));

            RegisterConverter<IndexMapping, HbmIndex>(c =>
                new HbmIndexConverter(c.Resolve<IHbmConverterServiceLocator>()));

            RegisterConverter<IndexManyToManyMapping, HbmIndexManyToMany>(c =>
                new HbmIndexManyToManyConverter(c.Resolve<IHbmConverterServiceLocator>()));

            RegisterConverter<ElementMapping, HbmElement>(c =>
                new HbmElementConverter(c.Resolve<IHbmConverterServiceLocator>()));

            RegisterConverter<OneToManyMapping, HbmOneToMany>(c =>
                new HbmOneToManyConverter());

            RegisterConverter<AnyMapping, HbmAny>(c =>
                new HbmAnyConverter(c.Resolve<IHbmConverterServiceLocator>()));

            RegisterConverter<MetaValueMapping, HbmMetaValue>(c =>
                new HbmMetaValueConverter());

            // collection relationships
            // FIXME: What does this need to convert as?
            RegisterConverter<ICollectionRelationshipMapping>(c =>
                new HbmCollectionRelationshipConverter(c.Resolve<IHbmConverterServiceLocator>()));

            RegisterConverter<ManyToOneMapping, HbmManyToOne>(c =>
                new HbmManyToOneConverter(c.Resolve<IHbmConverterServiceLocator>()));

            RegisterConverter<ManyToManyMapping, HbmManyToMany>(c =>
                new HbmManyToManyConverter(c.Resolve<IHbmConverterServiceLocator>()));

            // subclasses
            RegisterConverter<SubclassMapping, HbmSubclass>(c =>
                new HbmSubclassConverter(c.Resolve<IHbmConverterServiceLocator>()));

            RegisterConverter<FilterMapping, HbmFilter>(c =>
                new HbmFilterConverter());
            */

            RegisterConverter<FilterDefinitionMapping, HbmFilterDef>(c =>
                new HbmFilterDefinitionConverter());

            /*
            // FIXME: What does this need to convert as?
            RegisterConverter<StoredProcedureMapping>(c =>
                new HbmStoredProcedureConverter(c.Resolve<IHbmConverterServiceLocator>()));

            RegisterConverter<TuplizerMapping, HbmTuplizer>(c =>
                new HbmTuplizerConverter());
            */
        }

        private void RegisterIdConverters()
        {
            RegisterConverter<IIdentityMapping, object>(c =>
                new HbmIdentityBasedConverter(c.Resolve<IHbmConverterServiceLocator>()));

            /*
            RegisterConverter<IdMapping, HbmId>(c =>
                new HbmIdConverter(c.Resolve<IHbmConverterServiceLocator>()));
            */

            RegisterConverter<CompositeIdMapping, HbmCompositeId>(c =>
                new HbmCompositeIdConverter(c.Resolve<IHbmConverterServiceLocator>()));

            /*
            RegisterConverter<GeneratorMapping, HbmGenerator>(c =>
                new HbmGeneratorConverter());
            */

            RegisterConverter<KeyPropertyMapping, HbmKeyProperty>(c =>
                new HbmKeyPropertyConverter(c.Resolve<IHbmConverterServiceLocator>()));

            RegisterConverter<KeyManyToOneMapping, HbmKeyManyToOne>(c =>
                new HbmKeyManyToOneConverter(c.Resolve<IHbmConverterServiceLocator>()));
        }

        private void RegisterComponentConverters()
        {
            /*
            // FIXME: What does this need to convert as?
            // FIXME: Needs an explicit import or to be fully explicit here, depending on whether we need  both IComponentMapping types...
            RegisterConverter<IComponentMapping, HbmComponent>(c =>
                new HbmComponentConverter(c.Resolve<IHbmConverterServiceLocator>()));

            // FIXME: What does this need to convert as?
            RegisterConverter<ReferenceComponentMapping>(c =>
                new HbmReferenceComponentConverter(c.Resolve<IHbmConverterServiceLocator>()));
            */
        }

        private void RegisterConverter<F, H>(Func<Container, object> instantiate)
            where F : IMapping
        {
            Register<IHbmConverter<F, H>>(instantiate);
        }
    }
}
