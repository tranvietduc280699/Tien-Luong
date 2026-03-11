import MsEnum from "@/enums/enums.js";
///
/// loại thành phần
///
export const componentTypeOptions = [
  { label: "Thông tin nhân viên", value: MsEnum.ComponentType.EmployeeInfo },
  { label: "Chấm công", value: MsEnum.ComponentType.Timekeeping },
  { label: "Doanh số", value: MsEnum.ComponentType.Sales },
  { label: "KPI", value: MsEnum.ComponentType.KPI },
  { label: "Sản phẩm", value: MsEnum.ComponentType.Product },
  { label: "Lương", value: MsEnum.ComponentType.Salary },
  { label: "Thuế TNCN", value: MsEnum.ComponentType.PersonalIncomeTax },
  { label: "Bảo hiểm CĐ", value: MsEnum.ComponentType.InsuranceUnion },
  { label: "Khác", value: MsEnum.ComponentType.Other },
];

///
/// tính chất
///

export const optionProperties = [
  { label: "Thu nhập", value: MsEnum.Nature.Income },
  { label: "Khấu trừ", value: MsEnum.Nature.Deduction },
  { label: "Khác", value: MsEnum.Nature.Other },
];
//RadioBox

export const ListNatureOption = [
  { label: "Chịu thuế", value: MsEnum.NatureOption.Taxable },
  { label: "Miễn thuế toàn phần", value: MsEnum.NatureOption.FullyExempt },
  { label: "Miễn Thuế một phần", value: MsEnum.NatureOption.PartiallyExempt },
];

///
/// kiểu giá trị
///

export const valueTypeOption = [
  { label: "Số", value: MsEnum.ValueTypeEnum.Number },
  { label: "Tiền tệ", value: MsEnum.ValueTypeEnum.Currency },
  { label: "Phần trăm", value: MsEnum.ValueTypeEnum.Percent },
  { label: "Chữ", value: MsEnum.ValueTypeEnum.Text },
  { label: "Ngày", value: MsEnum.ValueTypeEnum.Date },
];
///
/// giá trị
///
// trong cùng đơn vị công tác
export const listAggregationScope = [
  {
    label: "Trong cùng đơn vị công tác",
    value: MsEnum.AggregationScopeEnum.SameDepartment,
  },
  { label: "Dưới quyền", value: MsEnum.AggregationScopeEnum.Subordinate },
  {
    label: "Thuộc cơ cấu tổ chức",
    value: MsEnum.AggregationScopeEnum.OrganizationStructure,
  },
];

// chọn thành phần lương để cộng giá trị
export const sumValueOptions = [
  {
    label: "Chọn thành phần lương để cộng giá trị",
    value: MsEnum.SumValueOptionKey.addSalaryComponentValue,
  },
  {
    label: "Thời gian phân công",
    value: MsEnum.SumValueOptionKey.assignmentTime,
  },
  {
    label: "Lương làm thêm giờ",
    value: MsEnum.SumValueOptionKey.overtimeSalary,
  },
];
///
/// Hiển thị trên phiếu lương
///
export const listShowSalaryOptions = [
  { label: "Có", value: MsEnum.SalaryShowOption.Yes },
  { label: "Không", value: MsEnum.SalaryShowOption.No },
  {
    label: "Chỉ hiển thị nếu giá trị khác 0",
    value: MsEnum.SalaryShowOption.ShowIfNotZero,
  },
];