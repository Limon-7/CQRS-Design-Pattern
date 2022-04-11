import React from "react";
import { Button, Card, Icon, Image } from "semantic-ui-react";
import { IActivity } from "../../../app/model/iActivity";
interface IProps {
  activity: IActivity;
  setEditMode: (editMode: boolean) => void;
  setSelectedActivity: (activity: IActivity|null) => void;

}
function ActivityDetails({ activity, setEditMode , setSelectedActivity}: IProps) {
  const { id, title, date, description, category } = activity;
  return (
    <Card fluid>
      <Image
        src={`/assets/categoryImages/${category}.jpg`}
        wrapped
        ui={false}
      />
      <Card.Content>
        <Card.Header>{title}</Card.Header>
        <Card.Meta>
          <span>{date}</span>
        </Card.Meta>
        <Card.Description>{description}</Card.Description>
      </Card.Content>
      <Card.Content extra>
        <Button.Group widths={2}>
          <Button basic color="blue" onClick={()=>setEditMode(true)}>
            Edit
          </Button>
          <Button basic color="grey" onClick={()=>setSelectedActivity(null)}>
            Cancel
          </Button>
        </Button.Group>
      </Card.Content>
    </Card>
  );
}

export default ActivityDetails;
