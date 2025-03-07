using System;
using FluentNHibernate.Diagnostics;
using NHibernate.Cfg;

namespace FluentNHibernate.Cfg
{
    /// <summary>
    /// Fluent mapping configuration
    /// </summary>
    public class MappingConfiguration
    {
        bool mergeMappings;
        readonly IDiagnosticLogger logger;
        PersistenceModel model;
        IMappingApplicationStrategy mappingApplicationStrategy;

        public MappingConfiguration()
            : this(new NullDiagnosticsLogger())
        {}

        public MappingConfiguration(IDiagnosticLogger logger)
        {
            this.logger = logger;

            FluentMappings = new FluentMappingsContainer();
            AutoMappings = new AutoMappingsContainer();
            HbmMappings = new HbmMappingsContainer();

            UsePersistenceModel(new PersistenceModel());
            model.SetLogger(logger);
        }

        public MappingConfiguration UsePersistenceModel(PersistenceModel persistenceModel)
        {
            model = persistenceModel;
            return this;
        }

        public MappingConfiguration UseMappingApplicationStrategy(IMappingApplicationStrategy mappingApplicationStrategy)
        {
            this.mappingApplicationStrategy = mappingApplicationStrategy;
            return this;
        }

        /// <summary>
        /// Fluent mappings
        /// </summary>
        public FluentMappingsContainer FluentMappings { get; private set; }

        /// <summary>
        /// Automatic mapping configurations
        /// </summary>
        public AutoMappingsContainer AutoMappings { get; private set; }

        /// <summary>
        /// Hbm mappings
        /// </summary>
        public HbmMappingsContainer HbmMappings { get; private set; }

        /// <summary>
        /// Get whether any mappings of any kind were added
        /// </summary>
        public bool WasUsed
        {
            get
            {
                return FluentMappings.WasUsed ||
                       AutoMappings.WasUsed ||
                       HbmMappings.WasUsed;
            }
        }

        /// <summary>
        /// Applies any mappings to the NHibernate Configuration
        /// </summary>
        /// <param name="cfg">NHibernate Configuration instance</param>
        public void Apply(Configuration cfg)
        {
            Apply(cfg, 1);
        }
        /// <summary>
        /// Applies any mappings to the NHibernate Configuration
        /// </summary>
        /// <param name="cfg">NHibernate Configuration instance</param>
        /// <param name="degreeOfParallelism">the number of threads to use to perform startup</param>
        public void Apply(Configuration cfg, int degreeOfParallelism)
        {
            foreach (var autoMapping in AutoMappings)
                autoMapping.SetLogger(logger);

            if (mergeMappings)
            {
                foreach (var autoModel in AutoMappings)
                    autoModel.MergeMappings = true;

                model.MergeMappings = true;
            }

            HbmMappings.Apply(cfg);
            FluentMappings.Apply(model);
            AutoMappings.Apply(cfg, model);
            model.DegreeOfParallelism = degreeOfParallelism;
            if (mappingApplicationStrategy != null)
                model.MappingApplicationStrategy = mappingApplicationStrategy;
            model.Configure(cfg);
        }

        public MappingConfiguration MergeMappings()
        {
            mergeMappings = true;

            return this;
        }
    }
}