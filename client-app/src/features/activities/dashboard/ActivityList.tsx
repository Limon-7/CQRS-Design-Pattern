import React from "react";
import { Item, Image, Button, Label, Segment } from "semantic-ui-react";
import { IActivity } from "../../../app/model/iActivity";

interface IProps {
  activities: IActivity[];
  selectActivity: (id: string) => void;
  deleteActivity: (id: string) => void;
}
const ActivityList: React.FC<IProps> = ({ activities, selectActivity, deleteActivity }) => {
  return (
    <Segment clearing>
      <Item.Group divided>
        {activities.map(
          ({ title, date, description, city, venue, category, ...props }) => (
            <Item key={props.id}>
              <Item.Content>
                <Item.Header as="a">{title}</Item.Header>
                <Item.Meta>{date}</Item.Meta>
                <Item.Description>
                  <div>{description}</div>
                  <div>
                    {city}, {venue}
                  </div>
                </Item.Description>
                <Item.Extra>
                  <Button floated="right" color="blue" onClick={()=>selectActivity(props.id)}>
                    View
                  </Button>
                  <Button floated="right" color="red" onClick={()=>deleteActivity(props.id)}>
                    Delete
                  </Button>
                  <Label basic>{category}</Label>
                </Item.Extra>
              </Item.Content>
            </Item>
          )
        )}
      </Item.Group>
    </Segment>
  );
};

export default ActivityList;
