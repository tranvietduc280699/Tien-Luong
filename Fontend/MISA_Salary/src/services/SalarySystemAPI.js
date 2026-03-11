import BaseService from "./baseService/baseService";
class SalarySystemService extends BaseService {
  constructor() {
    super("SalarySystem");
  }
}

export default new SalarySystemService();