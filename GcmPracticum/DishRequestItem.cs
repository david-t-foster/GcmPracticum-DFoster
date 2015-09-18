namespace GcmPracticum
{
    // keep track of a dish item and count
    public class DishRequestItem
    {
        public static DishRequestItem Create(int item, int count)
        {
            return new DishRequestItem()
            {
                Item = item,
                Count = count
            };
        }

        public int Item { get; private set; }
        public int Count { get; private set; }

        public int Offset {  get { return Item - 1; } }

        private DishRequestItem()
        {

        }
    }
}
