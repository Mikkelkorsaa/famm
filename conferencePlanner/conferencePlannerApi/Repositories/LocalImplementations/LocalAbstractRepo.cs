using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;
using Org.BouncyCastle.Utilities;

namespace conferencePlannerApi.Repositories.LocalImplementations
{
	public class LocalAbstractRepo : IAbstractRepo
	{
		List<Abstract> _abstracts = new()
			 {
					 new Abstract
					 {
							 Id = 1,
							 ConferenceId = 101,
							 SenderName = "Dr. Sarah Johnson",
							 PresenterEmail = "s.johnson@university.edu",
							 CoAuthors = new List<string> { "Dr. Michael Chen", "Prof. Emma Williams" },
							 Organization = "University of Technology",
							 Title = "Machine Learning Applications in Climate Change Prediction",
							 KeyValues = "climate modeling, machine learning, neural networks, weather prediction",
							 AbstractText = "This study presents a novel approach to climate change prediction using advanced machine learning techniques. We demonstrate how neural networks can be applied to historical climate data to improve the accuracy of future climate projections. Our results show a 15% improvement in prediction accuracy compared to traditional methods.",
							 Category = "Machine Learning",
							 Picture = "string.Empty",
							 Reviews = new List<Review>()
					 },

					 new Abstract
					 {
							 Id = 2,
							 ConferenceId = 101,
							 SenderName = "Prof. David Martinez",
							 PresenterEmail = "dmartinez@research.org",
							 CoAuthors = new List<string> { "Dr. Lisa Cooper" },
							 Organization = "Research Institute of Biotechnology",
							 Title = "Novel CRISPR Applications in Treating Genetic Disorders",
							 KeyValues = "CRISPR, gene editing, genetic disorders, therapeutic applications",
							 AbstractText = "Our research explores innovative applications of CRISPR technology in treating rare genetic disorders. Through a series of controlled experiments, we have developed a modified CRISPR-Cas9 system that shows promising results in correcting specific genetic mutations with minimal off-target effects.",
							 Category = "Biotechnology",
							 Picture = "string.Empty",
							 Reviews = new List<Review>()
					 },

					 new Abstract
					 {
							 Id = 3,
							 ConferenceId = 102,
							 SenderName = "Dr. Rachel Anderson",
							 PresenterEmail = "anderson.r@sustaintech.com",
							 CoAuthors = new List<string> { "Dr. James Wilson", "Dr. Maria Garcia", "Dr. Tom Baker" },
							 Organization = "SustainTech Solutions",
							 Title = "Sustainable Urban Development: A Smart City Framework",
							 KeyValues = "smart cities, sustainability, urban planning, IoT integration",
							 AbstractText = "This paper presents a comprehensive framework for implementing smart city solutions in urban development. By integrating IoT sensors, renewable energy systems, and adaptive traffic management, our approach has demonstrated significant improvements in urban efficiency and sustainability. Case studies from three major cities show reductions in energy consumption and traffic congestion.",
							 Category = "Urban Development",
							 Picture = "string.Empty",
							 Reviews = new List<Review>()
					 }
			 };
		private int _lastId = 3;

		public async Task<Abstract?> GetByIdAsync(int id)
			=> await Task.FromResult(_abstracts.FirstOrDefault(a => a.Id == id));
		public async Task<IEnumerable<Abstract>> GetAllAsync()
			=> await Task.FromResult(_abstracts);

		public async Task<Abstract> CreateAsync(Abstract @abstract)
		{
			var newAbstract = @abstract with
			{
				Id = ++_lastId,
				Picture = new PictureModel(){ PictureBase64 = @abstract.Picture.PictureBase64 }
			};
			
			_abstracts.Add(newAbstract);
			return await Task.FromResult(newAbstract);
		}

		public async Task<Abstract?> UpdateAsync(Abstract @abstract)
		{
			var index = _abstracts.FindIndex(a => a.Id == @abstract.Id);
			if (index == -1) return null;

			var updatedAbstract = @abstract with { Id = _abstracts[index].Id, Picture = new PictureModel(){ PictureByteArray = @abstract.Picture.PictureByteArray } };
			_abstracts[index] = updatedAbstract;
			return await Task.FromResult<Abstract?>(@abstract);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var index = _abstracts.FindIndex(a => a.Id == id);
			if (index == -1) return await Task.FromResult(false);

			_abstracts.RemoveAt(index);
			return await Task.FromResult(true);
		}
	}
}
