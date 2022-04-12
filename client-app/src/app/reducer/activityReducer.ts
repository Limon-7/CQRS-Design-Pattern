import { Dispatch } from "react";
import api from "../api/api";
import { IActivity } from "../model/iActivity";

export enum ActionType {
  GET_ACTIVITY_PENDING = "GET_ACTIVITY_PENDING",
  GET_ACTIVITY_SUCCESS = "GET_ACTIVITY_SUCCESS",
  GET_ACTIVITY_FAIL = "GET_ACTIVITY_FAIL",
}
export enum ActionTypeStatus {
  IDLE = "IDLE",
  LOADING = "LOADING",
  FAILED = "FAILED",
}
interface actionPending {
  type: ActionType.GET_ACTIVITY_PENDING;
}

interface actionSuccess {
  type: ActionType.GET_ACTIVITY_SUCCESS;
  payload: IActivity[];
}

interface actionFail {
  type: ActionType.GET_ACTIVITY_FAIL;
  payload: null;
}

export type Action = actionPending | actionSuccess | actionFail;

interface State {
  activities: IActivity[];
  loading: ActionTypeStatus;
  error: any | null;
}

const initialState = {
  activities: [],
  loading: ActionTypeStatus.IDLE,
  error: null,
};

export const activityReducer = (
  state: State = initialState,
  action: Action
): State => {
  switch (action.type) {
    case ActionType.GET_ACTIVITY_PENDING:
      return {
        loading: ActionTypeStatus.LOADING,
        activities: [],
        error: null,
      };
    case ActionType.GET_ACTIVITY_SUCCESS:
      return {
        loading: ActionTypeStatus.IDLE,
        activities: action.payload,
        error: null,
      };
    case ActionType.GET_ACTIVITY_FAIL:
      return {
        loading: ActionTypeStatus.IDLE,
        error: action.payload,
        activities: [],
      };
    default:
      return state;
  }
};

export default activityReducer;

export const getActivities = () => {
  return async (dispatch: Dispatch<Action>) => {
    dispatch({ type: ActionType.GET_ACTIVITY_PENDING});
    try {
      let data= await api.Activities.list();
      dispatch({
        type: ActionType.GET_ACTIVITY_SUCCESS,
        payload: data  
    });
    } catch (error:any) {
      dispatch({
        type: ActionType.GET_ACTIVITY_SUCCESS,
        payload: error 
    });
    }
  };
};
