import React, { SyntheticEvent } from "react";
import { Item, Image, Button, Label, Segment } from "semantic-ui-react";
import { IActivity } from "../../../app/model/iActivity";

interface IProps {
  activities: IActivity[];
  selectActivity: (id: string) => void;
  deleteActivity: (e: SyntheticEvent<HTMLButtonElement>, id: string) => void;
  submitting: boolean;
  target: string;
}
const ActivityList: React.FC<IProps> = ({
  activities,
  selectActivity,
  deleteActivity,
  submitting,
  target,
}) => {
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
                  <Button
                    floated="right"
                    color="blue"
                    onClick={() => selectActivity(props.id)}
                  >
                    View
                  </Button>
                  <Button
                    name={props.id}
                    loading={target === props.id && submitting}
                    floated="right"
                    color="red"
                    onClick={(e) => deleteActivity(e, props.id)}
                  >
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
