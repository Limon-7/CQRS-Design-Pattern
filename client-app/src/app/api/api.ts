import axios, { AxiosResponse } from 'axios';
import { IActivity } from '../model/iActivity';

axios.defaults.baseURL='http://localhost:5000/api';

const responseBody=<T>(response:AxiosResponse<T>)=>response.data;

const requests={
  get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
  put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
  del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
}

const Activities = {
  list: () => axios.get<IActivity[]>('/activities')
      .then(responseBody),
  details: (id: string) => requests.get<IActivity>(`/activities/${id}`),
  create: (activity: IActivity) => requests.post<void>('/activities', activity),
  update: (activity: IActivity) => requests.put<void>(`/activities/${activity.id}`, activity),
  delete: (id: string) => requests.del<void>(`/activities/${id}`),
  attend: (id: string) => requests.post<void>(`/activities/${id}/attend`, {})
}


export default{
  Activities
}