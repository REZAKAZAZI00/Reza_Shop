

namespace Shop.DataLayer.Enttities.Wallet
{
    public class WalletType
    {
        public WalletType()
        {

        }
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [Required]
        [MaxLength(150)]
        public string  TypeTitle { get; set; }


        #region Relation
        public virtual List<Wallet> Wallets { get; set; }
        #endregion
    }
}
