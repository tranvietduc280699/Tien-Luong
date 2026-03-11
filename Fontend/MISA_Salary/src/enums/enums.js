
// loại thành phần
const ComponentType = {
  EmployeeInfo: 1,
  Timekeeping: 2,
  Sales: 3,
  KPI: 4,
  Product: 5,
  Salary: 6,
  PersonalIncomeTax: 7,
  InsuranceUnion: 8,
  Other: 9,
};
// tính chất
const Nature = {
  Income: 1, // thu nhập
  Deduction: 2, // khấu trừ
  Other: 3, // khác
};
const NatureOption = {
  Taxable: 1, // Chịu thuế
  FullyExempt: 2, // Miễn thuế toàn phần
  PartiallyExempt: 3, // Miễn thuế 1 phần
};
// kiểu giá trị
const ValueTypeEnum = {
    Number: 1,        // Số
    Currency: 2,      // Tiền tệ
    Percent: 3,       // Phần trăm
    Text: 4,          // Chữ
    Date: 5           // Ngày
}
// giá trị 
const CalculationMethodEnum = {
    AutoSum: 1,        // Tự động cộng tổng giá trị của các nhân viên
    CustomFormula: 2   // Tính theo công thức tự đặt
}
const AggregationScopeEnum = {
    SameDepartment: 1,     // Trong cùng đơn vị công tác
    Subordinate: 2,        // Dưới quyền
    OrganizationStructure: 3 // Thuộc cơ cấu tổ chức
}
// hiển thị trên phiếu lương
const SalaryShowOption = {
    Yes: 1,                   // Có
    No: 2,                    // Không
    ShowIfNotZero: 3     // chỉ hiển thị nếu giá trị khác 0
}
const SumValueOptionKey = {
  addSalaryComponentValue: 1, // chọn thành phần lương để cộng giá trị
  assignmentTime: 2, // thời gian phân công
  overtimeSalary: 3, // luông làm thêm giờ
};
// các button bên phải (sidebar)
const MenuSidebar = {
    Overview: 1,            // Tổng quan
    SalaryComponent: 2,     // Thành phần lương
    SalaryTemplate: 3,      // Mẫu bảng lương
    SalaryData: 4,          // Dữ liệu tính lương
    SalaryCalculation: 5,   // Tính lương
    Payment: 6,             // Chi trả
    Report: 7,              // Báo cáo
    Settings: 8             // Thiết lập
};

export default {
  ComponentType,
  Nature,
  NatureOption,
  ValueTypeEnum,
  CalculationMethodEnum,
  AggregationScopeEnum,
  SalaryShowOption,
  SumValueOptionKey,
  MenuSidebar
};
