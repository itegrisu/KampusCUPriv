using Application.Features.Base;
using Core.Entities;

namespace Application.Helpers.UpdateRowNo
{
    public class UpdateRowNoHelper<TResponse, TEntity>
        where TResponse : BaseResponse, new()
        where TEntity : BaseEntity, IHasRowNo
    {
        public async Task<TResponse> UpdateRowNo(List<TEntity> lst, TEntity select, bool isUp)
        {
            if (select == null)
            {
                return new()
                {
                    Title = "İşlem sırasında hata ile karşılaşıldı",
                    Message = "İlgili kayıt bulunamadı",
                    IsValid = false,
                };
            }

            int? indexSelect = lst.IndexOf(select);
            if (indexSelect == null)
            {
                return new()
                {
                    Title = "İşlem sırasında hata ile karşılaşıldı",
                    Message = "İlgili kayıt bulunamadı",
                    IsValid = false,
                };
            }

            if (indexSelect == 0 && isUp)
            {
                return new()
                {
                    Title = "Geçersiz işlem",
                    Message = "Yukarı almak istediğiniz kayit birinci sırada",
                    IsValid = false,
                };
            }

            if (lst.Last() == select && !isUp)
            {
                return new()
                {
                    Title = "Geçersiz işlem",
                    Message = "Aşağı almak istediğiniz kayıt son sırada",
                    IsValid = false,
                };
            }

            TEntity selectOther;

            if (isUp)//true => yukarı, false => aşağı
                selectOther = lst[indexSelect.Value - 1];
            else
                selectOther = lst[indexSelect.Value + 1];

            int tempRowNo = select.RowNo;
            select.RowNo = selectOther.RowNo;
            selectOther.RowNo = tempRowNo;

            return new()
            {
                Title = "İşlem başarılı",
                Message = "Seçilen Kayıt Başarıyla Taşındı",
                IsValid = true,
            };
        }
    }
}
