﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Sdl.Community.ExcelTerminology.Insights
{
    public sealed class TelemetryService
    {
        private readonly TelemetryClient _telemetryClient;

        private static readonly Lazy<TelemetryService> _lazy =
            new Lazy<TelemetryService>(() => new TelemetryService());

        private TelemetryService()
        {
            _telemetryClient = new TelemetryClient ();
            TelemetryConfiguration.Active.ContextInitializers.Add(new UserSessionInitializer());

            HandleExcetion();
        }


        public void Init()
        {
            
        }


        public void AddMetric(string name, double value, IDictionary<string,string> properties= null)
        {
            _telemetryClient.TrackMetric(name, value, properties);
        }

        public void AddException(Exception ex, IDictionary<string,string> properties=null)
        {
            _telemetryClient.TrackException(ex, properties);

        }

        public void AddEvent(string name, IDictionary<string,string> properties = null)
        {
            _telemetryClient.TrackEvent(name, properties);
        }

        public void TrackPage(string page)
        {
            _telemetryClient.TrackPageView(page);
        }
        public static TelemetryService Instance => _lazy.Value;

        private void HandleExcetion()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var excTelemetry = new ExceptionTelemetry((Exception)e.ExceptionObject)
            {
                SeverityLevel = SeverityLevel.Critical,
                HandledAt = ExceptionHandledAt.Unhandled
            };

            _telemetryClient.TrackException(excTelemetry);

            _telemetryClient.Flush();
        }

    }
}
