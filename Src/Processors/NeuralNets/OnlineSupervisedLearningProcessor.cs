using System.Threading;
using System.Threading.Tasks;
using Accord.Neuro.Learning;
using kalev.EnsembleLearning.Data;
using Xer.Messaginator;

namespace kalev.EnsembleLearning.Processors
{
    public class OnlineSupervisedLearningProcessor : MessageProcessor<SupervisedData>
    {
        private readonly ISupervisedLearning learner;

        public OnlineSupervisedLearningProcessor(IMessageSource<SupervisedData> messageSource,
                                           ISupervisedLearning learner) : base(messageSource)
        {
            this.learner = learner;
        }

        public override string Name => typeof(OnlineSupervisedLearningProcessor).Name;

        protected override Task ProcessMessageAsync(MessageContainer<SupervisedData> receivedMessage, 
                                                    CancellationToken cancellationToken)
        {
            SupervisedData data = receivedMessage;

            data.Error = learner.Run(data.Data, data.Label);
            
            return Task.CompletedTask;           
        }
    }
}