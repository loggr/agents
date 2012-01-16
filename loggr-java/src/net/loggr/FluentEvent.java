package net.loggr;

public class FluentEvent extends FluentEventBase<FluentEvent> {

	@Override
	public FluentEvent text(String text) {
		// TODO Auto-generated method stub
		return super.text(text);
	}

	@Override
	public FluentEvent link(String link) {
		// TODO Auto-generated method stub
		return super.link(link);
	}

	@Override
	public FluentEvent source(String source) {
		// TODO Auto-generated method stub
		return super.source(source);
	}

	@Override
	public FluentEvent user(String user) {
		// TODO Auto-generated method stub
		return super.user(user);
	}

	@Override
	public FluentEvent tags(String tags) {
		// TODO Auto-generated method stub
		return super.tags(tags);
	}

	@Override
	public FluentEvent value(double value) {
		// TODO Auto-generated method stub
		return super.value(value);
	}

	@Override
	public FluentEvent data(String data) {
		// TODO Auto-generated method stub
		return super.data(data);
	}

	@Override
	public FluentEvent dataType(net.loggr.DataType dataType) {
		// TODO Auto-generated method stub
		return super.dataType(dataType);
	}

	@Override
	public Event event() {
		// TODO Auto-generated method stub
		return super._event;
	}

	// inherits for creating a simple API

}
