using System.Collections.Generic;

namespace SalesWebMvc.Models.ViewModels
{
    public class AdviceFormViewModel //: IEnumerable<Advice>
    {
        //public int Id { get; set; }        
        //public string Advice { get; set; }
        //public IEnumerable<Advice> Advices { get; set; }
        public ICollection<Advice> Advices { get; set; } = new List<Advice>();

        public AdviceFormViewModel()
        {            
        }

        public AdviceFormViewModel(Advice advices)
        {
            Advices.Add(advices);
        }



        /*public IEnumerator<Advice> GetEnumerator()
        {
            yield return (IEnumerator)GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            //throw new System.NotImplementedException();
            return null;
        }*/
    }
}
