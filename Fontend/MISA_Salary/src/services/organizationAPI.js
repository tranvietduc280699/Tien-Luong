import BaseService from "./baseService/baseService";
class OrganizationService extends BaseService {
  constructor() {
    super("Organization");
  }
}
export default new OrganizationService();