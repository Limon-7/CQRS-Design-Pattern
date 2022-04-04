export enum ActionType {
  TAB_SELECTED = 'TAB_SELECTED'
}

interface selectTab {
  type: ActionType.TAB_SELECTED;
  payload: string ;
}

export type Action = selectTab;

interface State {
  currentTab: string 
}

const initialState:State = {
  currentTab: "tab1"
}

const toggleReducer = (state = initialState, action: Action) => {
  switch (action.type) {
    case ActionType.TAB_SELECTED: {
      return {
        ...state,
        currentTab: action.payload,
      };
    }
    default:
      return state;
  }
};

export default toggleReducer;