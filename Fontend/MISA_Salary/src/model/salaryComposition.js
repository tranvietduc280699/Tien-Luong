export default class SalaryComposition {
        // Tên thành phần
        SalaryCompositionName = "";

        // Mã thành phần
        SalaryCompositionCode = "";

        // Đơn vị áp dụng (Dạng mảng ID khi ở FE, Join thành chuỗi khi gửi BE)
        OrganizationUnitData = [];

        // Loại thành phần
        ComponentType = "";

        // Tính chất (Thu nhập/Khấu trừ/Khác)
        Nature = null;

        // Tùy chọn tính chất (Chịu thuế/Miễn thuế...)
        TaxableType = null;

        // Giảm trừ khi tính thuế
        IsDeductedOnTax = false;

        // Định mức
        Quota = "";

        // Cho phép giá trị tính vượt quá định mức
        IsExceedQuota = false;

        // Kiểu giá trị (Số/Tiền tệ/Phần trăm...)
        SalaryValueType = null;
        
        // giá trị ( AutoSum/CustomFormula)
        SalaryValue = ""; 

        // Phạm vi tổng hợp (Trong cùng đơn vị công tác...)
        ValueArrgegatitonScope = "";

        // Thành phần lương để cộng giá trị (Khi chọn AutoSum)
        SumValue = "";

        // Công thức tự đặt (Khi chọn CustomFormula)
        RecipeSuggestion = "";

        // Mô tả
        SalaryDescription = "";

        // Hiển thị trên phiếu lương
        ShowOnPayslip = null;

}