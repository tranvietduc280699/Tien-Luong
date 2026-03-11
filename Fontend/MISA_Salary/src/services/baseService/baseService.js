import service from "./serviceFatory";
export default class BaseService {
  constructor(controller) {
    this.controller = controller;
  }
  ///
  /// thành phần lương
  ///
  getById(id){
    return service.get(`${this.controller}/GetById/${id}`);
  }
  async GetByKeyWord(keyWord){
    return await service.get(`${this.controller}/GetByKeyWord`, {params:{keyWord}});
  }
  paging(PagingParam) {
    return service.post(`/${this.controller}/paging`, PagingParam);
  }
  delete(id) {
    return service.delete(`/${this.controller}/${id}`);
  }
  insert(obj){
    return service.post(`/${this.controller}/Insert`, obj);
  }
  deleteMany(ids) {
    return service.post(`/${this.controller}/DeleteManyID`, ids);
  }
  update(obj){
    return service.post(`/${this.controller}/Update`,obj);
  }
  UpdateStatus(id, FieldValue){
    return service.post(`/${this.controller}/UpdateSingleField`, null,{params:{id,FieldValue}});
  }
 UpdateStatusMany(ids, status) {
  return service.post(`/${this.controller}/UpdateStatusMany`, null, {
    params: {
      ids,    
      status 
    },
    paramsSerializer: {
      indexes: null // Loại bỏ dấu [] và giữ nguyên tên key 'ids'
    }
  });
}
  getAll(){
    return service.get(`/${this.controller}/getAll`);
  }
}
